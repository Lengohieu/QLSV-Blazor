using Microsoft.JSInterop;

namespace QLSV.Data
{
    public static class JSInteropExt
    {
        public static async Task SaveAsFileAsync(this IJSRuntime js, string filename, byte[] data, string type = "application/octet-stream")
        {
            await js.InvokeAsync<object>("JSInteropExt.saveAsFile", filename, type, Convert.ToBase64String(data));
        }
    }
}
