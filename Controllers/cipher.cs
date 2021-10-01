using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;
using DataStructures;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EDll_L4_AFPE_DAVH.Controllers
{
    [Route("api/")]
    [ApiController]
    public class cipher : Controller
    {
        public static IWebHostEnvironment _environment;
        public cipher(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public class FileUPloadAPI
        {
            public IFormFile FILE { get; set; }
        }
        public class Key
        {
            public string KEY { get; set; }
        }

        public IActionResult Post(string method,[FromForm] FileUPloadAPI? objFile, [FromForm] string key)
        {
            try
            {
                if (objFile.FILE != null)
                {
                    if (objFile.FILE.Length > 0)
                    {
                        string uniqueFileName = objFile.FILE.FileName + "-" + Guid.NewGuid().ToString();
                        if (!Directory.Exists(_environment.WebRootPath + "\\Upload\\"))
                        {
                            Directory.CreateDirectory(_environment.WebRootPath + "\\Upload\\");
                        }

                        using (FileStream fileStream = System.IO.File.Create(_environment.WebRootPath + "\\Upload\\" + uniqueFileName))
                        {
                            objFile.FILE.CopyTo(fileStream);
                            fileStream.Flush();
                        }

                        byte[] content = System.IO.File.ReadAllBytes(_environment.WebRootPath + "\\Upload\\" + uniqueFileName);

                        if (method == "cesar")
                        {
                             ICaesarCipher CaesarCipher = new Caesar();

                            byte[] textCompressed = CaesarCipher.Cipher(Encoding.UTF8.GetString(content));



                            CompressModel compressObj = new CompressModel
                            {
                                originalFileName = objFile.FILE.FileName,
                                CompressedFileName_Route = name + ".huff" + "-->" + _environment.WebRootPath + "\\Upload\\",
                                rateOfCompression = Math.Round((Convert.ToDouble(textCompressed.Length) / Convert.ToDouble(content.Length)), 2).ToString(),
                                compressionFactor = Math.Round((Convert.ToDouble(content.Length) / Convert.ToDouble(textCompressed.Length)), 2).ToString(),
                                reductionPercentage = Math.Round((Convert.ToDouble(textCompressed.Length) / Convert.ToDouble(content.Length)) * 100, 2).ToString() + "%",
                            };

                            Singleton.Instance.compressions.InsertAtStart(compressObj);

                            return File(textCompressed, "application/text", name + ".huff");
                        }
                        else if (method == "lzw")
                        {
                            ILZWCompressor compressor = new LZW();

                            byte[] textCompressed = compressor.Compress(Encoding.UTF8.GetString(content));



                            CompressModel compressObj = new CompressModel
                            {
                                originalFileName = objFile.FILE.FileName,
                                CompressedFileName_Route = name + ".lzw" + "-->" + _environment.WebRootPath + "\\Upload\\",
                                rateOfCompression = Math.Round((Convert.ToDouble(textCompressed.Length) / Convert.ToDouble(content.Length)), 2).ToString(),
                                compressionFactor = Math.Round((Convert.ToDouble(content.Length) / Convert.ToDouble(textCompressed.Length)), 2).ToString(),
                                reductionPercentage = Math.Round((Convert.ToDouble(textCompressed.Length) / Convert.ToDouble(content.Length)) * 100, 2).ToString() + "%",
                            };

                            Singleton.Instance.compressions.InsertAtStart(compressObj);

                            return File(textCompressed, "application/text", name + ".lzw");
                        }
                        else
                        {
                            return StatusCode(500);
                        }
                    }
                    else
                    {
                        return StatusCode(500);
                    }
                }
                else
                {
                    return StatusCode(500);
                }
            }
            catch
            {
                return StatusCode(500);
            }

        }

    }
}
