# opentok-winforms-sample

Very basic OpenTok sample using WinForms as the GUI framework.

The code in here is similiar to [Basic Video Chat Sample](https://github.com/opentok/opentok-windows-sdk-samples/tree/master/BasicVideoChat) from official windows sdk samples repo but using WinForms instead of WPF.

In order to implement the VideoRenderer, a WPFHost component has been used. This component reuse the same VideoRenderer used in WPF application.
