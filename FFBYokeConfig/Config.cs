using System;
using System.Linq;
using System.Data;
using System.Text;
using System.Drawing;
using System.IO.Ports;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using System.Threading;
using System.IO;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;


namespace FFBYokeConfig
{
    [XmlRoot("Root")]
    public class Config
    {
        #region Configuration 
        //-- 
        [XmlElement("FFB_Auto_Save")]
        public bool FFB_Auto_Save { get; set; }

        [XmlElement("FFB_Port_Name")]
        public string FFB_Port_Name { get; set; }

        [XmlElement("FFB_Port_Delays")]
        public int FFB_Port_Delays{ get; set; }

        //Devices Fonfic
        #endregion
    }
}//--------------------------------------------------
