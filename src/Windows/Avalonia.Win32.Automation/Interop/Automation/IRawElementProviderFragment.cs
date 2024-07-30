using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using Avalonia.Win32.Automation.Marshalling;

namespace Avalonia.Win32.Interop.Automation
{
    [Guid("670c3006-bf4c-428b-8534-e1848f645122")]
    internal enum NavigateDirection
    {
        Parent,
        NextSibling,
        PreviousSibling,
        FirstChild,
        LastChild,
    }

// NOTE: This interface needs to be public otherwise Navigate is never called. I have no idea
// why given that IRawElementProviderSimple and IRawElementProviderFragmentRoot seem to get
// called fine when they're internal, but I lost a couple of days to this.
#if NET8_0_OR_GREATER
    [GeneratedComInterface(StringMarshalling = StringMarshalling.Utf8)]
#else
    [ComImport()]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
#endif
    [Guid("f7063da8-8359-439c-9297-bbc5299a7d87")]
    internal partial interface IRawElementProviderFragment
    {
        IRawElementProviderFragment? Navigate(NavigateDirection direction);
#if NET8_0_OR_GREATER
    [return: MarshalUsing(typeof(SafeArrayMarshaller<int>))]
#endif
        IReadOnlyList<int>? GetRuntimeId();
        Rect BoundingRectangle();
#if NET8_0_OR_GREATER
        [return: MarshalUsing(typeof(SafeArrayMarshaller<IRawElementProviderSimple>))]
#endif
        IReadOnlyList<IRawElementProviderSimple>? GetEmbeddedFragmentRoots();
        void SetFocus();
        IRawElementProviderFragmentRoot? FragmentRoot();
    }
}
