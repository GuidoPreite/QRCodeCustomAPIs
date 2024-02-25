using Microsoft.Xrm.Sdk;
using System;
using static QRCodeCustomAPIs.Plugins.PayloadGenerator;

namespace QRCodeCustomAPIs.Plugins
{
    public class GenerateQRCodeUrl : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            string url = context.InputParameters["Url"] as string;
            bool success = false;
            string base64Result = "";
            try
            {
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                Url payloadUrl = new Url(url);
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(payloadUrl);
                PngByteQRCode png = new PngByteQRCode(qrCodeData);
                byte[] byteResult = png.GetGraphic(20);
                base64Result = Convert.ToBase64String(byteResult);
                success = true;
            }
            catch { }
            context.OutputParameters["Success"] = success;
            context.OutputParameters["Base64Result"] = base64Result;
        }
    }
}
