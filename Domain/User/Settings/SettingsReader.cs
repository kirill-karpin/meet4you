using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace User.Settings
{
    public class SettingsReader
    {
        /// <summary>
        /// Получает соль с проекта WebApp
        /// </summary>
        /// <returns>Строка соли, если всё ОК. В противном случае - Null</returns>
        public string GetSalt()
        {
            try
            {
                string dir = Directory.GetCurrentDirectory();
                // Вызываться по факту будет из проекта WebApp (WebApi, ибо его переименовывали)
                string xmlFile = dir + "\\Config\\Settings.xml";

                XmlDocument doc = new XmlDocument();
                doc.Load(xmlFile);

                foreach (XmlNode node in doc.DocumentElement)
                {
                    string salt = node["SaltString"].InnerText;
                    return salt;
                }

                // сюда попадём, только если xml файл не содержит нужной строки
                return null;
            } 
            catch (FileNotFoundException ex)
            {
                Random r = new Random();
                string randomSalt =  r.Next().ToString();

                return randomSalt;
            }

        }
    }
}
