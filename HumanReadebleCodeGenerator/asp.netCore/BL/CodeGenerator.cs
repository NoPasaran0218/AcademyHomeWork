using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace asp.netCore.BL
{
    public interface ICodeGenerator
    {
        string GetCode(int id);
    }
    public class CodeGenerator:ICodeGenerator
    {
        private string prefix;
        private static  string [] Codes { get; set; }
        public CodeGenerator()
            :this("BJ")
        {

        }

        public CodeGenerator(string prefix)
        {
            this.prefix = prefix;
            Codes = File.ReadLines(@"Files/code.txt").ToArray();
        }

        public string GetCode(int id)
        {
            DateTime date = DateTime.Now;
            string code = prefix + (date.Day / 10).ToString() + (date.Day % 10).ToString() + (date.Month / 10).ToString() + (date.Month % 10).ToString() +"-"+ Codes[(id - 1) % 100];
            return code;
        }

        public static string GetCodeStatic(int id)
        {
            DateTime date = DateTime.Now;
            string code =  Codes[(id - 1) % 100];
            return code;
        }
    }
}
