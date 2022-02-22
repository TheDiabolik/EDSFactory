﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace EDSFactory
{
    static partial class Serialization 
    {
        public class SerializeClass
        {
             public static void Serialize(string xmlFilePath, dynamic eaevs)
            {
                try
                {
                    if (!Directory.Exists(Path.GetDirectoryName(xmlFilePath)))
                        Directory.CreateDirectory(Path.GetDirectoryName(xmlFilePath));

                    using (FileStream fs = new FileStream(xmlFilePath, FileMode.Create, FileAccess.Write))
                    {
                        XmlSerializer s = new XmlSerializer(eaevs.GetType());
                        s.Serialize(fs, eaevs);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ExceptionMessages.SerilizationSettingsExceptionMessage, ex);
                }
            }
            public static dynamic DeSerialize(string xmlFilePath, dynamic eaevs)
            {
                try
                {
                    if (!Directory.Exists(Path.GetDirectoryName(xmlFilePath)))
                        Directory.CreateDirectory(Path.GetDirectoryName(xmlFilePath)); 

                    using (FileStream fs = new FileStream(xmlFilePath, FileMode.Open, FileAccess.Read))
                    {
                        XmlSerializer s = new XmlSerializer(eaevs.GetType());
                        eaevs = (dynamic)s.Deserialize(fs);

                        return eaevs;
                    }

                }
                catch (Exception ex)
                {
                    throw new Exception(ExceptionMessages.DeSerilizationSettingsExceptionMessage, ex);
                }
            }

   
        } 
    }
}
