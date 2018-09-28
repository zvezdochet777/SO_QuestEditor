using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SOHelpEditor
{
    public class Token
    {
        public string type { get; set; }
        public string value { get; set; }
    }

    public class LittleCard
    {
        public string value = "";
    }

    public class Card
    {
        public string title { get; set; }
        public int id { get; set; }
        public List<Token> full_card { get; set; }
        public LittleCard little_card = new LittleCard();
    }

    public class Chapter
    {
        public string name { get; set; }
        public int id { get; set; }
        public List<Card> cards { get; set; }
        public List<Token> full_card { get; set; }
    }

    public class RootObject
    {
        public List<Chapter> chapters { get; set; }

        public bool removeChapterByID(int id, out List<string> image_names, out List<string> text_names)
        {
            int charapter_id = id / 100 * 100;
            image_names = new List<string>();
            text_names = new List<string>();
            foreach (Chapter charapter in this.chapters)
            {
                if (charapter.id != charapter_id) continue;
                if (charapter_id == id)
                {
                    foreach (Token token in charapter.full_card)
                    {
                        switch(token.type)
                        {
                            case "image":    image_names.Add(token.value); break;
                            case "text":     text_names.Add(token.value);  break;
                            case "subtitle": text_names.Add(token.value);  break;
                        }
                    }
                    charapter.full_card = new List<Token>();
                    this.chapters.Remove(charapter);
                    return true;
                }
                else
                {
                    foreach (Card card in charapter.cards)
                    {
                        if (card.id == id)
                        {
                            foreach (Token token in card.full_card)
                            {
                                switch (token.type)
                                {
                                    case "image": image_names.Add(token.value); break;
                                    case "text": text_names.Add(token.value); break;
                                    case "subtitle": text_names.Add(token.value); break;
                                }
                            }
                            charapter.cards.Remove(card);
                            card.full_card = new List<Token>();
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public bool clearDataByID(int id)
        {
            int charapter_id = id / 100 * 100;
            foreach (Chapter charapter in this.chapters)
            {
                if (charapter.id != charapter_id) continue;
                if (charapter_id == id)
                {
                    charapter.full_card = new List<Token>();
                    return true;
                }
                else
                {
                    if (charapter.cards != null)
                    foreach (Card card in charapter.cards)
                    {
                        if (card.id == id)
                        {
                            card.full_card = new List<Token>();
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public bool setElementName(int id, string name)
        {
            int charapter_id = id / 100 * 100;
            foreach (Chapter charapter in this.chapters)
            {
                if (charapter.id != charapter_id) continue;
                if (charapter_id == id)
                {
                    charapter.name = name;
                    return true;
                }
                else
                {
                    if (charapter.cards != null)
                        foreach (Card card in charapter.cards)
                        {
                            if (card.id == id)
                            {
                                card.title = name;
                                return true;
                            }
                        }
                }
            }
            return false;
        }

        public bool moveElement(int id, int changeIndex)
        {
            int charapter_id = id / 100 * 100;            
            foreach (Chapter charapter in this.chapters)
            {
                if (charapter.id != charapter_id) continue;
                if (charapter_id == id)
                {
                    int index = chapters.IndexOf(charapter);
                    chapters.Remove(charapter);
                    int new_index = Math.Max(0, Math.Min(index + changeIndex, chapters.Count - 1));
                    chapters.Insert(new_index, charapter);
                    return true;
                }
                else
                {
                    if (charapter.cards != null)
                        foreach (Card card in charapter.cards)
                        {
                            if (card.id == id)
                            {
                                int index = charapter.cards.IndexOf(card);
                                charapter.cards.Remove(card);
                                int new_index = Math.Max(0, Math.Min(index + changeIndex, charapter.cards.Count - 1));
                                charapter.cards.Insert(new_index, card);
                                return true;
                            }
                        }
                }
            }
            return false;
        }
    }

    public class SOImage
    {
        public string name { get; set; }
        public string path_russian { get; set; }
        public string path_english { get; set; }
        public string path { get; set; }
    }

    public class ImageToken
    {
        public string path;
        public System.Drawing.Image image;
    }

    public class ImagesObject
    {
        public List<SOImage> images { get; set; }
        public string comment { get; set; }
    }
}
