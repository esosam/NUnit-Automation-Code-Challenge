using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.Resources;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;

namespace TestCodeChallenge.pom
{
    class Config
    {
        private string _json_config_path;
        private string _json_file_path;

        public Config()
        {
            _json_file_path= TestCodeChallange.pom.Consts._json_path;
            _json_config_path = Path.Combine(Environment.CurrentDirectory, _json_file_path);
        }

        public string GetConfigItem(string node)
        {
            JObject JasonObj = GetAllJSonText();
            return JasonObj[node].ToString();
        }

        public string GetConfigPath()
        {
            return Environment.CurrentDirectory;
        }

        public List<JToken> GetConfigItems(string node)
        {
            JObject JasonObj = GetAllJSonText();
            return JasonObj.SelectToken(node).ToList();
        }
        protected JObject GetAllJSonText()
        {
            return JObject.Parse(File.ReadAllText(_json_config_path));
        }
    }
}
