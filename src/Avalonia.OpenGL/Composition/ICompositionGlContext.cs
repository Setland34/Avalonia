using System;
using System.Threading.Tasks;
using Avalonia.Metadata;
using Avalonia.Rendering.Composition;

namespace Avalonia.OpenGL.Composition;

[NotClientImplementable, Unstable]
public interface ICompositionGlContext : IAsyncDisposable
{
    /// <summary>
    /// The associated compositor
    /// </summary>
    Compositor Compositor { get; }
    /// <summary>
    /// The OpenGL context
    /// </summary>
    IGlContext Context { get; }
    /// <summary>
    /// Creates a swapchain that can draw into provided CompositionDrawingSurface instance
    /// </summary>
    /// <param name="surface">The surface to draw into</param>
    /// <param name="size">The pixel size for the textures generated by the swapchain</param>
    /// <param name="onDispose">The callback to be called when the swapchain is about to be disposed</param>
    ICompositionGlSwapchain CreateSwapchain(CompositionDrawingSurface surface, PixelSize size, Action? onDispose = null);
}

[NotClientImplementable, Unstable]
public interface ICompositionGlSwapchain : IAsyncDisposable
{
    /// <summary>
    /// Attempts to get the next texture in the swapchain. If all textures in the swapchain are currently queued for
    /// presentation, returns null
    /// </summary>
    ICompositionGlSwapchainLockedTexture? TryGetNextTexture();
    /// <summary>
    /// Gets the the next texture in the swapchain or extends the swapchain.
    /// Note that calling this method without waiting for previous textures to be presented can introduce
    /// high GPU resource usage
    /// </summary>
    ICompositionGlSwapchainLockedTexture GetNextTextureIgnoringQueueLimits();
    /// <summary>
    /// Asynchronously gets the next texture from the swapchain once one becomes available
    /// You should not be calling this method while your IGlContext is current
    /// </summary>
    ValueTask<ICompositionGlSwapchainLockedTexture> GetNextTextureAsync();
    /// <summary>
    /// Resizes the swapchain to a new pixel size
    /// </summary>
    void Resize(PixelSize size);
}

[NotClientImplementable, Unstable]
public interface ICompositionGlSwapchainLockedTexture : IDisposable
{
    /// <summary>
    /// The task you can use to wait for presentation to complete on the render thread
    /// </summary>
    public Task Presented { get; }
    
    /// <summary>
    /// The texture you are expected to render into. You can bind it to GL_COLOR_ATTACHMENT0 of your framebuffer.
    /// Note that the texture must be unbound before this object is disposed 
    /// </summary>
    public int TextureId { get; }
}