using System;

namespace NumberInWords
{
    class Program
    {
        static void Main(string[] args)
        {
            string x;
            do
            {
                Console.WriteLine("Input your number please: ");
                x = Console.ReadLine();
                Translator tr = new Translator(x);
                Console.WriteLine(tr.convertUtility(tr.getY()));
                Console.WriteLine("Next number? (y/n)");
            }
            while (Console.ReadLine() != "n");

        }
    }

    class Translator
    {
        private string x;
        private long y;
        private bool male = true;

        public Translator(string x)
        {
            this.x = x;
            y = StringToInt(this.x);
        }

        public long getY()
        {
            return y;
        }

        private long StringToInt(string x)
        {
            long res = -1;
            try
            {
                res = Convert.ToInt64(x);
                if (res < 0)
                {
                    Console.WriteLine("Use positive values.");
                }
            }
            catch (FormatException fe)
            {
                Console.WriteLine(fe.Message);
            }
            catch (OverflowException oe)
            {
                Console.WriteLine(oe.Message);
            }
            return res;
        }

        public string convertUtility(long y)
        {
            string res = "";
            long z = 0;
            if (y >= 1e12)
            {
                z = y / (long)1e12;
                res += pluralForms(z, WordSamples.trillions);
                z = y % (long)1e12;
                if (z > 0)
                {
                    res += " " + convertUtility(z);
                }
            }
            else
            {
                if (y >= 1e9)
                {
                    z = y / (long)1e9;
                    res += pluralForms(z, WordSamples.billions);
                    z = y % (long)1e9;
                    if (z > 0)
                    {
                        res += " " + convertUtility(z);
                    }
                }
                else
                {
                    if (y >= 1e6)
                    {
                        z = y / (long)1e6;
                        res += pluralForms(z, WordSamples.millions);
                        z = y % (long)1e6;
                        if (z > 0)
                        {
                            res += " " + convertUtility(z);
                        }
                    }
                    else
                    {
                        if (y >= 1e3)
                        {
                            z = y / (long)1e3;
                            male = false;
                            res += pluralForms(z, WordSamples.thousands);
                            male = true;
                            z = y % (long)1e3;
                            if (z > 0)
                            {
                                res += " " + convertUtility(z);
                            }
                        }
                        else {
                            if (y >= 0)
                            {
                                if (y < 20) res += Parser(y);
                                else
                                if (y < 100)
                                {
                                    res += Parser(y / 10 * 10);
                                    res += " " + Parser(y % 10);
                                }
                                else
                                {
                                    res += Parser(y / 100 * 100);
                                    if (y % 100 >= 20)
                                    {
                                        res += " " + Parser(y / 10 * 10 - y / 100 * 100);
                                        res += " " + Parser(y % 10);
                                    }
                                    else
                                    res += " " + Parser(y % 100);
                                }
                            }
                        }
                    }
                }
            }
            return res;
        } 

        private string pluralForms(long z, string[] decpl)
        {
            string res = "";
            res += convertUtility(z);
            if (z == 1 || z % 10 == 1 && z != 11 && (z - z / 100 * 100) != 11) res += " " + decpl[1];
            else if (z % 10 > 0 && (z < 5 || z % 10 < 5 && (z - z / 100 * 100) / 10 != 1)) res += " " + decpl[2];
            else res += " " + decpl[3];
            return res;
        }

        private string Parser(long x)
        {
            string res = "";
            switch (x)
            {
                case 0:
                    res = y == 0 ? WordSamples.zero : "";
                    break;
                case 1:
                    res = male ? WordSamples.firstMale : WordSamples.firstFemale;
                    break;
                case 2:
                    res = male ? WordSamples.secondMale : WordSamples.secondFemale;
                    break;
                case 20:
                case 30:
                case 40:
                case 50:
                case 60:
                case 70:
                case 80:
                case 90:
                    res = WordSamples.tens[x / 10 - 1];
                    break;
                case 100:
                case 200:
                case 300:
                case 400:
                case 500:
                case 600:
                case 700:
                case 800:
                case 900:
                    res = WordSamples.hundreds[x / 100];
                    break;
                default:
                    res = WordSamples.from3till19[x - 2];
                    break;
            }
            return res;
        }
    }
}

