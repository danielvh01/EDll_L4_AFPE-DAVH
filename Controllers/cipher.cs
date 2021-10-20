﻿using Microsoft.AspNetCore.Http;
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

                            byte[] textCompressed = CaesarCipher.Cipher(content, key);

                            return File(textCompressed, "application/text", name.Substring(0, name.Length - 4) + ".csr");
                        }
                        else if (method == "zigzag")
                        {
                            string name = objFile.FILE.FileName;

                            int Key = 0;
                            if(int.TryParse(key, out Key))
                            {
                                IZigzagCipher chipher = new Zigzag();

                                byte[] textCompressed = chipher.Cipher(content, Key);
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

        public IActionResult SDESCipher(string name, [FromForm] FileUPloadAPI objFile, [FromForm] string key)
        {
            
            int secretKey = int.Parse(key);
            if (secretKey < 1024)
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

                        ISDES cipher = new SDES(Path.GetDirectoryName(@"Configuration\"));
                        byte[] textCiphered = cipher.Cipher(content, secretKey);
                        string names = System.IO.File.ReadAllText(Environment.CurrentDirectory + "\\Names.txt");
                        System.IO.File.WriteAllText(Environment.CurrentDirectory + "\\Names.txt",names + objFile.FILE.FileName + "|" + name + ".sdes\n");
                        return File(textCiphered, "application/text", name + ".sdes");

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

        [HttpPost("sdes/decipher/")]

        public IActionResult SDESDeCipher([FromForm] FileUPloadAPI objFile, [FromForm] string key)
        {

            int secretKey = int.Parse(key);
            if (secretKey < 1024)
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

                        ISDES cipher = new SDES(Path.GetDirectoryName(@"Configuration\"));

                        byte[] textCiphered = cipher.Decipher(content, secretKey);
                        string originalFileName = objFile.FILE.FileName;
                        string names = System.IO.File.ReadAllText(Environment.CurrentDirectory + "\\Names.txt");
                        string[] files = names.Split("\n");
                        string nameTofile = "";
                        string newContent = "";
                        bool encontrado = false;
                        for(int i = 0; i < files.Length - 1; i++)
                        {
                            string[] filenames = files[i].Split("|");
                            if(filenames[1] == objFile.FILE.FileName && !encontrado)
                            {
                                nameTofile = filenames[0];
                                encontrado = true;
                            }
                            else
                            {
                                newContent += files[i] + "\n";
                            }
                        }
                        System.IO.File.WriteAllText(Environment.CurrentDirectory + "\\Names.txt", newContent);
                        return File(textCiphered, "application/text", nameTofile);
                            
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

                            byte[] textDecompressed = CaesarCipher.Decipher(content, key);

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

                                byte[] textDecompressed = chipher.Decipher(content, Key);

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
