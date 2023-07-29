﻿using FellowOakDicom;
using FellowOakDicom.Imaging;
using FellowOakDicom.Imaging.Codec;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convert
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var file = DicomFile.Open(@"Input.dcm");  
         
            var newFile = file.Clone(DicomTransferSyntax.ExplicitVRLittleEndian);

            newFile.Save(@"output.dcm");
            
        }
       
    }
}
