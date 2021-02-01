using System;
using System.IO;
using GraZaDuzoZaMalo.Model;
using System.Runtime.Serialization.Formatters.Binary;

namespace AppGraZaDuzoZaMaloCLI
{
    public class SerializajcaBitowa : ZapiszGre
    {
        private BinaryFormatter formatter = new BinaryFormatter();
        public SerializajcaBitowa()
        {
            zapis = "zapis.bin";
        }

        protected override Gra Wczytywanie(Stream stream)
        {
            return (Gra)formatter.Deserialize(stream);
        }

        protected override void Zapisywanie(Stream stream, Gra gra)
        {
            formatter.Serialize(stream, gra);
        }
    }

    public abstract class ZapiszGre
    {
        protected String zapis;
        public void SerializacjaGry(Gra gra)
        {
            using (var stream = new FileStream(zapis, FileMode.Create, FileAccess.Write))
            {
                Zapisywanie(stream, gra);
            }
        }
        public Gra WczytajGre()
        {
            using (var stream = new FileStream(zapis, FileMode.Open, FileAccess.Read))
            {
                return Wczytywanie(stream);
            }
        }
        public void UsunZapis()
        {
            if (zapisDostepny)
            {
                File.Delete(zapis);
            }
        }
        public bool zapisDostepny { get => File.Exists(zapis); }
        protected abstract void Zapisywanie(Stream stream, Gra gra);
        protected abstract Gra Wczytywanie(Stream stream);
    }
}
