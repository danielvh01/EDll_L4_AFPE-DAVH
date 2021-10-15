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
        [HttpPost("cipher/{method}")]
        public IActionResult Post(string method,[FromForm] FileUPloadAPI objFile, [FromForm] string key)
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
                            string name = objFile.FILE.FileName;
                            ICaesarCipher CaesarCipher = new Caesar();

                            byte[] textCompressed = CaesarCipher.Cipher(Encoding.GetEncoding(28591).GetString(content), key);

                            return File(textCompressed, "application/text", name.Substring(0, name.Length - 4) + ".csr");
                        }
                        else if (method == "zigzag")
                        {
                            string name = objFile.FILE.FileName;

                            int Key = 0;
                            if(int.TryParse(key, out Key))
                            {
                                IZigzagCipher chipher = new Zigzag();

                                byte[] textCompressed = chipher.Cipher(Encoding.GetEncoding(28591).GetString(content), Key);
                                return File(textCompressed, "application/text", name.Substring(0, name.Length - 4) + ".zz");
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

        [HttpPost("sdes/cipher/{name}")]

        public IActionResult SDESCipher(string method, [FromForm] FileUPloadAPI objFile, [FromForm] string key)
        {

        }

        [HttpPost("decipher")]
        public IActionResult Post([FromForm] FileUPloadAPI objFile, [FromForm] string key)
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

                        if (objFile.FILE.FileName.EndsWith("csr"))
                        {
                            string name = objFile.FILE.FileName;
                            string defName = name.Substring(0, name.Length - 4) + ".txt";
                            ICaesarCipher CaesarCipher = new Caesar();

                            byte[] textDecompressed = CaesarCipher.Decipher(Encoding.GetEncoding(28591).GetString(content), key);

                            return File(textDecompressed, "application/text", defName);
                        }
                        if (objFile.FILE.FileName.EndsWith("zz"))
                        {
                            int Key = 0;
                            if (int.TryParse(key, out Key))
                            {
                                string name = objFile.FILE.FileName;
                                string defName = name.Substring(0, name.Length - 3) + ".txt";
                                IZigzagCipher chipher = new Zigzag();

                                byte[] textDecompressed = chipher.Decipher(Encoding.GetEncoding(28591).GetString(content), Key);

                                return File(textDecompressed, "application/text", defName);
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
