using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;
using DataStructures;
using ICSharpCode.SharpZipLib.Zip;
using System.Collections.Generic;
using System.Linq;

namespace EDll_L4_AFPE_DAVH.Controllers
{
    [Route("api/")]
    [ApiController]
    public class cipher : Controller
    {
        #region Constructors and class objects   
        public static IWebHostEnvironment _environment;
        public cipher(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public class FileUPloadAPI
        {
            public IFormFile FILE { get; set; }            
        }
        public class FileUPloadAPI2
        {
            public IFormFileCollection FILE { get; set; }
        }

        #endregion

        #region CESAR & ZIGZAG 
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
                            ICipher<string> CaesarCipher = new Caesar();

                            byte[] textCompressed = CaesarCipher.Cipher(content, key);

                            return File(textCompressed, "application/text", name.Substring(0, name.Length - 4) + ".csr");
                        }
                        else if (method == "zigzag")
                        {
                            string name = objFile.FILE.FileName;

                            int Key = 0;
                            if(int.TryParse(key, out Key))
                            {
                                ICipher<int> chipher = new Zigzag();

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
                            ICipher<string> CaesarCipher = new Caesar();

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
                                ICipher<int> chipher = new Zigzag();

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

        #endregion

        #region SDES

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

                        ICipher<int> cipher = new SDES(Path.GetDirectoryName(@"Configuration\"));
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

                        ICipher<int> cipher = new SDES(Path.GetDirectoryName(@"Configuration\"));

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

        #endregion

        #region RSA
        [HttpGet("rsa/keys/{p}/{q}")]
        public IActionResult RSAKeys(string p, string q)
        {
            if (IsPrime(int.Parse(p)) && IsPrime(int.Parse(q))){
                RSA keygen = new RSA();
                var keys = keygen.GenKeys(int.Parse(p), int.Parse(q));
                if (!Directory.Exists(_environment.WebRootPath + "\\TempFiles\\"))
                {
                    Directory.CreateDirectory(_environment.WebRootPath + "\\TempFiles\\");
                }
                using (StreamWriter outputFile = new StreamWriter(Path.Combine(_environment.WebRootPath + "\\TempFiles\\", "private.key")))
                {                    
                        outputFile.Write(keys.Item1 + "," + keys.Item2);
                }
                using (StreamWriter outputFile = new StreamWriter(Path.Combine(_environment.WebRootPath + "\\TempFiles\\", "public.key")))
                {
                    outputFile.Write(keys.Item3 + "," + keys.Item2);
                }
                string filename = "RSAKeys.zip";
                string tempOutput = _environment.WebRootPath + "\\TempFiles\\" + filename;
                using (ZipOutputStream zipOutputStream = new ZipOutputStream(System.IO.File.Create(tempOutput)))
                {
                    zipOutputStream.SetLevel(9);

                    byte[] buffer = new byte[4096];
                    var filesList = new List<string>
                    {
                        _environment.WebRootPath + "\\TempFiles\\" + "private.key",
                        _environment.WebRootPath + "\\TempFiles\\" + "public.key"
                    };

                    for (int i = 0; i < filesList.Count; i++){
                        ZipEntry entry = new ZipEntry(Path.GetFileName(filesList[i]));
                        entry.DateTime = DateTime.Now;
                        entry.IsUnicodeText = true;
                        zipOutputStream.PutNextEntry(entry);

                        using (FileStream oFilestream = System.IO.File.OpenRead(filesList[i]))
                        {
                            int sourcebytes;
                            do {
                                sourcebytes = oFilestream.Read(buffer, 0, buffer.Length);
                                zipOutputStream.Write(buffer, 0, sourcebytes);
                            } while (sourcebytes > 0);
                        }
                    }
                    zipOutputStream.Finish();
                    zipOutputStream.Flush();
                    zipOutputStream.Close();
                }
                byte[] finalResult = System.IO.File.ReadAllBytes(tempOutput);
                if (System.IO.File.Exists(tempOutput))
                {
                    System.IO.File.Delete(tempOutput);
                }

                if (finalResult == null || !finalResult.Any()) {
                    throw new Exception(string.Format("Nothing found"));
                }
                return File(finalResult, "application/zip", filename);
            }
            else {
                Response.Clear();
                Response.StatusCode = 500; return Content("Error!" + "\n" + "Description: p & q must be prime numbers");                
            }
        }

        [HttpPost("rsa/{name}")]
        public IActionResult _RSA(string name, [FromForm] FileUPloadAPI2 objFile)
        {
            try
            {
                if (objFile.FILE != null)
                {
                    if (objFile.FILE.ElementAt(0).Length > 0 && objFile.FILE.ElementAt(1).Length > 0)
                    {
                        string uniqueFileName = objFile.FILE.ElementAt(0).FileName + "-" + Guid.NewGuid().ToString();
                        string uniqueFileName2 = objFile.FILE.ElementAt(1).FileName + "-" + Guid.NewGuid().ToString();
                        if (!Directory.Exists(_environment.WebRootPath + "\\Upload\\"))
                        {
                            Directory.CreateDirectory(_environment.WebRootPath + "\\Upload\\");
                        }
                        //Content
                        using (FileStream fileStream = System.IO.File.Create(_environment.WebRootPath + "\\Upload\\" + uniqueFileName))
                        {
                            objFile.FILE.ElementAt(0).CopyTo(fileStream);
                            fileStream.Flush();
                        }
                        //Keys
                        using (FileStream fileStream2 = System.IO.File.Create(_environment.WebRootPath + "\\Upload\\" + uniqueFileName2))
                        {
                            objFile.FILE.ElementAt(1).CopyTo(fileStream2);
                            fileStream2.Flush();
                        }
                        string keyFilecontent = System.IO.File.ReadAllText(_environment.WebRootPath + "\\Upload\\" + uniqueFileName2);
                        string[] splitKeys = keyFilecontent.Split(',');
                        int[] keys = { int.Parse(splitKeys[0]), int.Parse(splitKeys[1]) };
                        byte[] content = System.IO.File.ReadAllBytes(_environment.WebRootPath + "\\Upload\\" + uniqueFileName);
                        ICipher<int[]> _rsa = new RSA();
                        byte[] result = _rsa.Cipher(content, keys);
                        if(result != null)
                        {

                            return File(result, "application/text", name + ".txt");
                        }
                        else
                        {
                            Response.Clear();
                            Response.StatusCode = 500; return Content("Error!" + "\n" + "Description: The keys are too low for this file and it cannot be decrypted correctly.");
                        }
                    }
                    else
                    {
                        Response.Clear();
                        Response.StatusCode = 500; return Content("Error!" + "\n" + "Description: The File is empty.");
                    }
                }
                else
                {
                    Response.Clear();
                    Response.StatusCode = 500; return Content("Error!" + "\n" + "Description: Please upload a non-empty File.");
                }
            }
            catch (Exception e)
            {
                Response.Clear();
                Response.StatusCode = 500; return Content("Error!" + "\n" + "Description: " + e);
            }
        }

        bool IsPrime(int number)
        {
            if (number <= 1) return false;
            if (number == 2) return true;
            if (number % 2 == 0) return false;

            var boundary = (int)Math.Floor(Math.Sqrt(number));

            for (int i = 3; i <= boundary; i += 2)
                if (number % i == 0)
                    return false;

            return true;
        }


        #endregion
    }
}
