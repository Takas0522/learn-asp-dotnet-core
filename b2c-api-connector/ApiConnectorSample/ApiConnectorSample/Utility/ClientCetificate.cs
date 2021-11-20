using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ApiConnectorSample.Utility
{
    // https://docs.microsoft.com/ja-jp/azure/app-service/app-service-web-configure-tls-mutual-auth
    public class ClientCetificate : IClientCetificate
    {
        private readonly string cnName;
        private readonly string thumbprint;

        public ClientCetificate(
            IConfiguration config
        )
        {
            cnName = config["ClientCert:Cn"];
            thumbprint = config["ClientCert:Tp"];
        }

        public bool Validator(HttpRequest req)
        {
            var certHeader = req.Headers["X-ARR-ClientCert"];
            if (!string.IsNullOrEmpty(certHeader))
            {
                byte[] clientCertBytes = Convert.FromBase64String(certHeader);
                var certificate = new X509Certificate2(clientCertBytes);

                if (certificate == null) return false;

                // 1. Check time validity of certificate
                if (DateTime.Compare(DateTime.Now, certificate.NotBefore) < 0 || DateTime.Compare(DateTime.Now, certificate.NotAfter) > 0) return false;

                // 2. Check subject name of certificate
                bool foundSubject = false;
                string[] certSubjectData = certificate.Subject.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string s in certSubjectData)
                {
                    if (String.Compare(s.Trim(), $"CN={cnName}") == 0)
                    {
                        foundSubject = true;
                        break;
                    }
                }
                if (!foundSubject) return false;

                // 3. Check issuer name of certificate
                bool foundIssuerCN = false;
                string[] certIssuerData = certificate.Issuer.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string s in certIssuerData)
                {
                    if (String.Compare(s.Trim(), "CN=apiconnector.takassampleb2c.onmicrosoft.com") == 0)
                    {
                        foundIssuerCN = true;
                    }
                }
                if (!foundIssuerCN) return false;

                // 4. Check thumprint of certificate
                if (String.Compare(certificate.Thumbprint.Trim().ToUpper(), thumbprint) != 0) return false;

                return true;
            }
            return false;
        }
    }
}
