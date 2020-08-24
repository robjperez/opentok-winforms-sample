using OpenTok;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpentokWinForms
{
    public partial class Form1 : Form
    {
        public string API_KEY = "";
        public string TOKEN = "";
        public string SESSION_ID = "";

        public const int OTC_LOG_LEVEL_ALL = 100;

        VideoCapturer capturer;
        Session session;
        Publisher publisher;
        bool Disconnect = false;

        ConcurrentDictionary<Stream, Subscriber> subscriberByStream = new ConcurrentDictionary<Stream, Subscriber>();

        public void EnableLogging()
        {
            otc_logger_func X = (string message) =>
            {
                Console.WriteLine(message);
            };
            otc_log_enable(OTC_LOG_LEVEL_ALL);
            otc_log_set_logger_callback(X);
        }

        // Static interfaces
        [DllImport("opentok", EntryPoint = "otc_log_set_logger_callback", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void otc_log_set_logger_callback(otc_logger_func logger);

        [DllImport("opentok", EntryPoint = "otc_log_enable", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void otc_log_enable(int level);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void otc_logger_func(string message);

        public Form1()
        {
            InitializeComponent();

            wpfPublisherHost.Child = new OpenTok.VideoRenderer();
            wpfSubscriberHost.Child = new OpenTok.VideoRenderer();


            var cams = VideoCapturer.EnumerateDevices();
            var selectedcam = cams[0];
            capturer = selectedcam.CreateVideoCapturer(VideoCapturer.Resolution.High);
            publisher = new Publisher.Builder(Context.Instance)
            {
                Renderer = (IVideoRenderer)wpfPublisherHost.Child,
                Capturer = capturer
            }.Build();

            var mics = AudioDevice.EnumerateInputAudioDevices();
            AudioDevice.SetInputAudioDevice(mics[0]); // Go with first microphone in the list

            session = new Session.Builder(Context.Instance, API_KEY, SESSION_ID).Build();

            session.Connected += Session_Connected;
            session.Disconnected += Session_Disconnected;
            session.Error += Session_Error;
            session.ConnectionCreated += Session_ConnectionCreated;
            session.StreamReceived += Session_StreamReceived;
            session.StreamDropped += Session_StreamDropped;

            Closing += MainWindow_Closing;
        }

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            foreach (var subscriber in subscriberByStream.Values)
            {
                subscriber.Dispose();
            }
            publisher?.Dispose();
            capturer?.Dispose();
            session?.Dispose();
        }

        private void Session_ConnectionCreated(object sender, Session.ConnectionEventArgs e)
        {
            Console.WriteLine("Session connection created:" + e.Connection.Id);
        }

        private void Session_Error(object sender, Session.ErrorEventArgs e)
        {
            Console.WriteLine("Session error:" + e.ErrorCode);
        }

        private void Session_Disconnected(object sender, EventArgs e)
        {
            Console.WriteLine("Session disconnected");
            subscriberByStream.Clear();
        }

        // PLEASE NOTE THAT, WHEN USING WINFORMS, OPENTOK CALLBACKS ARE NOT CALLED IN THE MAIN THREAD
        // ANY CHANGE YOU WANT TO DO IN THE UI HAS TO BE ROUTED TO THE MT
        private void Session_Connected(object sender, EventArgs e)
        {
            Console.WriteLine("Session connected connection id:" + session.Connection.Id);
            try
            {
                session.Publish(publisher);
            }
            catch (OpenTokException ex)
            {
                Console.WriteLine("OpenTokException " + ex.ToString());
            }

        }

        private void Session_StreamReceived(object sender, Session.StreamEventArgs e)
        {
            Console.WriteLine("Session stream received");
            Subscriber subscriber = new Subscriber.Builder(Context.Instance, e.Stream)
            {
                Renderer = (IVideoRenderer)wpfSubscriberHost.Child
            }.Build();
            subscriberByStream.TryAdd(e.Stream, subscriber);

            try
            {
                session.Subscribe(subscriber);
            }
            catch (OpenTokException ex)
            {
                Console.WriteLine("OpenTokException " + ex.ToString());
            }
        }

        private void Session_StreamDropped(object sender, Session.StreamEventArgs e)
        {
            Console.WriteLine("Session stream dropped");
            var subscriber = subscriberByStream[e.Stream];
            if (subscriber != null)
            {
                Subscriber outsubs;
                subscriberByStream.TryRemove(e.Stream, out outsubs);

                try
                {
                    session.Unsubscribe(subscriber);
                }
                catch (OpenTokException ex)
                {
                    Console.WriteLine("OpenTokException " + ex.ToString());
                }
            }

        }

        private void Connect_Click(object sender, EventArgs e)
        {
            if (Disconnect)
            {
                Console.WriteLine("Disconnecting session");

                try
                {
                    session.Unpublish(publisher);
                    session.Disconnect();
                }
                catch (OpenTokException ex)
                {
                    Console.WriteLine("OpenTokException " + ex.ToString());
                }
            }
            else
            {
                Console.WriteLine("Connecting session");
                try
                {
                    session.Connect(TOKEN);
                }
                catch (OpenTokException ex)
                {
                    Console.WriteLine("OpenTokException " + ex.ToString());
                }
            }
            Disconnect = !Disconnect;
            Connect.Text = Disconnect ? "Disconnect" : "Connect";
        }
    }
}
