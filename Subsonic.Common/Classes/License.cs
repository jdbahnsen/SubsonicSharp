using System;
using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    public class License
    {
        [XmlIgnore]
        private DateTime? _licenseExpires;

        [XmlIgnore]
        private DateTime? _trialExpires;

        [XmlAttribute("email")]
        public string Email { get; set; }

        [XmlAttribute("licenseExpires")]
        public DateTime LicenseExpires
        {
            get => _licenseExpires.GetValueOrDefault();
            set => _licenseExpires = value;
        }

        [XmlAttribute("trialExpires")]
        public DateTime TrialExpires
        {
            get => _trialExpires.GetValueOrDefault();
            set => _trialExpires = value;
        }

        [XmlAttribute("valid")]
        public bool Valid { get; set; }

        public bool ShouldSerializeLicenseExpires()
        {
            return _licenseExpires.HasValue;
        }

        public bool ShouldSerializeTrialExpires()
        {
            return _trialExpires.HasValue;
        }
    }
}