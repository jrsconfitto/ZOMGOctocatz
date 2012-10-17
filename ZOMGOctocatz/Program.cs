using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;

namespace Octodexing {
    public class Program {
        static void Main(string[] args) {
            String OctocatsDirectory = "";

            if (args.Length == 0) {
                // Find the directory for the Octocats
                var Pictures = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
                OctocatsDirectory = Path.Combine(Pictures, "Octocatz");
                if (!Directory.Exists(OctocatsDirectory)) {
                    Directory.CreateDirectory(OctocatsDirectory);
                }
            }
            else if (args.Length == 1) {
                if (Directory.Exists(args[0])) {
                    OctocatsDirectory = args[0];
                }
                else {
                    try {
                        var info = Directory.CreateDirectory(args[0]);
                        OctocatsDirectory = info.FullName;
                        Console.WriteLine("Created home for the Octocats at `{0}`", OctocatsDirectory);
                    }
                    catch (Exception ex) {
                        Console.WriteLine("Could not create a home for the Octocats: {0}", ex.Message);
                        return;
                    }
                }
            }

            // Tell where we're putting the Octocatz
            Console.WriteLine("Sending Octocats to `{0}`", OctocatsDirectory);

            // This application will scrape all the Octodex cats and put them in a folder in your Pictures library
            var client = new RestClient("http://feeds.feedburner.com/Octocats");
            var request = new RestRequest(Method.GET);

            try {
                // async with deserialization
                var asyncHandle = client.ExecuteAsync<Atom>(request, response => {
                    response.Data.Entries.ForEach(entry => {
                        var content = entry.Content;
                        var octocatLink = entry.Content.Div.A.IMG.SRC;
                        var octocatFilePath = Path.Combine(OctocatsDirectory, entry.Title + ".png");

                        if (!File.Exists(octocatFilePath)) {
                            var gimmeOCTOCATZ = new RestClient(octocatLink);
                            var octocatRequest = new RestRequest(Method.GET);

                            var anotherAsyncHandle = gimmeOCTOCATZ.ExecuteAsync(octocatRequest, octocat => {
                                Console.WriteLine("Saving {0} to {1}", entry.Title, octocatFilePath);
                                File.WriteAllBytes(octocatFilePath, octocat.RawBytes);
                            });
                        }
                    });
                });
            }
            catch (Exception ex) {
                var message = ex.Message;
            }

            Console.WriteLine("Press enter to end the application");
            Console.WriteLine();
            Console.ReadLine();
        }
    }
}
