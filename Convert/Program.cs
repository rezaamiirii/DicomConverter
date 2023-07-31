using Dicom;
using Dicom.Imaging.Codec;
using FellowOakDicom;
using FellowOakDicom.Imaging;
using FellowOakDicom.Imaging.Codec;
using Microsoft.SqlServer.Server;
using Newtonsoft.Json;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Convert
{
    internal class Program
    {
        static void Main(string[] args)
        {
           
            var file = Dicom.DicomFile.Open(@"Input.dcm");

            var newFile=  file.ChangeTransferSyntax(Dicom.DicomTransferSyntax.ExplicitVRLittleEndian);

            newFile.Save(@"OutPut.dcm");

            var fileDateset = file.Dataset;

            Class1.ProcessImageFile(fileDateset, @"OutPut.dcm");

        }

    }
}
