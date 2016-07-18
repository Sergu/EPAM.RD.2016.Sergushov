using System;
using System.IO;
using System.Net;

namespace FileStreams
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Arguments: <source> <destination>");
                return;
            }

            string source = args[0];
            string destin = args[1];

            //ByteCopy(source, destin);
            //BlockCopy(source, destin);
            LineCopy(source, destin);
            //MemoryBufferCopy(source, destin);
            //WebClient();
        }

        public static void ByteCopy(string source, string destin)
        {
            int bytesCounter = 0;

            // TODO: Implement byte-copy here.

            using (var sourceStream = new FileStream(source,FileMode.Open))
            using (var destinStream = new FileStream(destin, FileMode.Append))
            using (var reader = new BinaryReader(sourceStream))
            using (var writer = new BinaryWriter(destinStream))
            {
                byte b;
                try
                {
                    while (true)
                    {
                        b = reader.ReadByte();// TODO: read byte
                        bytesCounter++;
                        writer.Write(b); // TODO: write byte
                    }
                }
                catch(EndOfStreamException ex)
                {

                }
            }


            Console.WriteLine("ByteCopy() done. Total bytes: {0}", bytesCounter);
        }

        public static void BlockCopy(string source, string destin)
        {
            // TODO: Implement block copy via buffer.

            using (var sourceStream = new FileStream(source, FileMode.Open))
            using (var destinStream = new FileStream(destin,FileMode.Append))
            using (var reader = new StreamReader(sourceStream))
            using (var writer = new StreamWriter(destinStream))
            {
                char[] buffer = new char[1024];
                int bytesRead = 0;
                do
                {
                    bytesRead = reader.ReadBlock(buffer,0,buffer.Length); // TODO: read in buffer

                    Console.WriteLine("BlockCopy(): writing {0} bytes.", bytesRead);
                    writer.Write(buffer, 0, bytesRead); // TODO: write to buffer
                }
                while (bytesRead == buffer.Length);
            }

        }

        public static void LineCopy(string source, string destin)
        {
            int linesCount = 0;

            // TODO: implement copying lines using StreamReader/StreamWriter.

            using (var sourceStream = new FileStream(source,FileMode.Open))
            using (var destinStream = new FileStream(destin,FileMode.Append))
            {
                using (var streamReader = new StreamReader(sourceStream))
                using (var streamWriter = new StreamWriter(destinStream))
                {
                    string line = null;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        streamWriter.WriteLine(line); // TODO: write line
                    }
                }
            }


            Console.WriteLine("LineCopy(): {0} lines.", linesCount);
        }

        public static void MemoryBufferCopy(string source, string destin)
        {
            /*
            var stringBuilder = new StringBuilder();

            string content;

            using (var textReader = (TextReader)new...) // TODO: use StreamReader here
            {
                content = ... // TODO: read entire file
            }

            using (var sourceReader = new ...) // TODO: Use StringReader here with content
            using (var sourceWriter = new ...) // TODO: Use StringWriter here with stringBuilder
            {
                string line = null;

                do
                {
                    line = ... // TODO: read line
                    if (line != null)
                    {
                        sourceWriter....(line); // TODO: write line
                    }

                } while (line != null);
            }

            Console.WriteLine("MemoryBufferCopy(): chars in StringBuilder {0}", stringBuilder.Length);

            const int blockSize = 1024;

            using (var stringReader = new ...)) // TODO: Use StringReader to read from stringBuilder.
            using (var memoryStream = new MemoryStream(blockSize))
            using (var streamWriter = new ...) // TODO: Compose StreamWriter with memory stream.
            using (var destinStream = new ...) // TODO: Use file stream.
            {
                char[] buffer = new char[blockSize];
                int bytesRead;

                do
                {
                    bytesRead = ...; // TODO: Read block from stringReader to buffer.
                    ...; // TODO: Write buffer to streamWriter.

                    // TODO: After implementing everythin check the content of NewTextFile. What's wrong with it, and how this may be fixed?

                    ...(memoryStream.GetBuffer()...; // TODO: write memoryStream.GetBuffer() content to destination stream.
                }
                while (bytesRead == blockSize);
            }

            */
        }

        public static void WebClient()
        {
            WebClient webClient = new WebClient();
            using (var stream = webClient.OpenRead("http://google.com"))
            {
                /*
                Console.WriteLine("WebClient(): CanRead = {0}", stream...); // TODO: print if it is possible to read from the stream
                Console.WriteLine("WebClient(): CanWrite = {0}", stream...); // TODO: print if it is possible to write to the stream
                Console.WriteLine("WebClient(): CanSeek = {0}", stream...); // TODO: print if it is possible to seek through the stream
                */

                // TODO: Save steam content to "google_request.txt" file.
            }
        }
    }
}