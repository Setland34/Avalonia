using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using Avalonia.Win32.Automation.Marshalling;

namespace Avalonia.Win32.Interop.Automation
{
    [Flags]
    [Guid("3d9e3d8f-bfb0-484f-84ab-93ff4280cbc4")]
    internal enum SupportedTextSelection
    {
        None,
        Single,
        Multiple,
    }
#if NET8_0_OR_GREATER
    [GeneratedComInterface(StringMarshalling = StringMarshalling.Utf8)]
#else
    [ComImport()]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
#endif
    [Guid("3589c92c-63f3-4367-99bb-ada653b77cf2")]
    internal partial interface ITextProvider
    {
#if NET8_0_OR_GREATER
        [return: MarshalUsing(typeof(SafeArrayMarshaller<ITextRangeProvider>))]
#endif
        IReadOnlyList<ITextRangeProvider> GetSelection();
#if NET8_0_OR_GREATER
        [return: MarshalUsing(typeof(SafeArrayMarshaller<ITextRangeProvider>))]
#endif
        IReadOnlyList<ITextRangeProvider> GetVisibleRanges();
        ITextRangeProvider RangeFromChild(IRawElementProviderSimple childElement);

        ITextRangeProvider RangeFromPoint(double X, double Y);

        //ITextRangeProvider RangeFromPoint([MarshalAs(UnmanagedType.Struct)] Point screenLocation);
        ITextRangeProvider DocumentRange();
        SupportedTextSelection SupportedTextSelection();
    }
}
