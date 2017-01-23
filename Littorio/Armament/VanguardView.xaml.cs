using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MahApps.Metro.Controls;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Littorio.Models;
using Fiddler;
using System.Runtime.CompilerServices;
using System.ComponentModel;

namespace Littorio.Armament
{
    /// <summary>
    /// Interaction logic for VanguardView.xaml
    /// </summary>
    public partial class VanguardView : UserControl, INotifyPropertyChanged
    {
        public VanguardView()
        {
            DataContext = this;
            InitializeComponent();
        }

        private const string Separator = "------------------------------------------------------------------";

        public static bool InstallCertificate()
        {
            if (!CertMaker.rootCertExists())
            {
                if (!CertMaker.createRootCert())
                    return false;

                if (!CertMaker.trustRootCert())
                    return false;
            }

            return true;
        }

        public static bool UninstallCertificate()
        {
            if (CertMaker.rootCertExists())
            {
                if (!CertMaker.removeFiddlerGeneratedCerts(true))
                    return false;
            }

            return true;
        }

        public void FiddlerOn(object sender, RoutedEventArgs e)
        {
            InstallCertificate();
            const FiddlerCoreStartupFlags flags =
                    FiddlerCoreStartupFlags.AllowRemoteClients |
                    FiddlerCoreStartupFlags.CaptureLocalhostTraffic |
                    FiddlerCoreStartupFlags.DecryptSSL |
                    FiddlerCoreStartupFlags.MonitorAllConnections |
                    FiddlerCoreStartupFlags.RegisterAsSystemProxy;

            FiddlerApplication.AfterSessionComplete += FiddlerApplication_AfterSessionComplete;

            Int32 iPort;
            if (Int32.TryParse(Proxy, out iPort))
                FiddlerApplication.Startup(iPort, flags);

        }

        private void FiddlerApplication_AfterSessionComplete(Session sess)
        {
            // Ignore HTTPS connect requests
            if (sess.RequestMethod == "CONNECT")
                return;


            if (sess == null || sess.oRequest == null || sess.oRequest.headers == null)
                return;

            string headers = sess.oRequest.headers.ToString();

            string contentType =
                sess.oRequest.headers.Where(hd => hd.Name.ToLower() == "content-type")
                    .Select(hd => hd.Name)
                    .FirstOrDefault();

            string reqBody = null;
            if (sess.RequestBody.Length > 0)
            {

                if (sess.requestBodyBytes.Contains((byte)0) || contentType.StartsWith("image/"))
                    reqBody = "b64_" + Convert.ToBase64String(sess.requestBodyBytes);
                else
                {
                    //reqBody = Encoding.Default.GetString(sess.ResponseBody);
                    reqBody = sess.GetRequestBodyAsString();
                }
            }

            // if you wanted to capture the response
            string respHeaders = sess.oResponse.headers.ToString();
            // 타입 체크를 한 뒤에 Encoding 하는 것이 어떨까?
            
            var respBody = Encoding.UTF8.GetString(sess.ResponseBody);
            string resBody = null;
            resBody = sess.GetResponseBodyAsString();
            // replace the HTTP line to inject full URL
            string firstLine = sess.RequestMethod + " " + sess.fullUrl + " " + sess.oRequest.headers.HTTPVersion;
            int at = headers.IndexOf("\r\n");
            if (at < 0)
                return;
            headers = firstLine + "\r\n" + headers.Substring(at + 1);

            string output = headers + "\r\n" +
                            (!string.IsNullOrEmpty(reqBody) ? reqBody + "\r\n" : string.Empty) +
                            Separator + "\r\n\r\n";

            Request = output;

            Response = respHeaders +  "\r\n" + resBody;
            
            
        }

        public void FiddlerOff(object sender, RoutedEventArgs e)
        {
            FiddlerApplication.AfterSessionComplete -= FiddlerApplication_AfterSessionComplete;

            if (FiddlerApplication.IsStarted())
                FiddlerApplication.Shutdown();
            UninstallCertificate();
        }

        private string _request;
        public string Request
        {
            get {
                return _request;
            }
            set {
                _request = value;
                OnPropertyChanged();
            }

        }

        private string _response;
        public string Response
        {
            get
            {
                return _response;
            }
            set
            {
                _response = value;
                OnPropertyChanged();
            }

        }

        private string _proxy;
        public string Proxy
        {
            get { return _proxy; }
            set
            {
                _proxy = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyname = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyname));
        }
    }
}
