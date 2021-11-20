using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ApiConnectorSample.Models
{
    public class SigninWithIdentityProviderRequest
    {
        [DataMember(Name = "email")]
        public string Email { get; set; }
        [DataMember(Name = "identities")]
        public IEnumerable<Identity> Identities { get; set; }
        [DataMember(Name = "displayName")]
        public string DisplayName { get; set; }
        [DataMember(Name = "givenName")]
        public string GivenName { get; set; }
        [DataMember(Name = "lastName")]
        public string LastName { get; set; }
        [DataMember(Name = "ui_locales")]
        public string UiLocales { get; set; }
    }

    public class Identity {
        [DataMember(Name = "signInType")]
        public string SignInType { get; set; }
        [DataMember(Name = "issuer")]
        public string Issuer { get; set; }
        [DataMember(Name = "issuerAssignedId")]
        public string IssuerAssignedId { get; set; }
    }
}
