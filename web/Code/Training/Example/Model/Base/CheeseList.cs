using System;
using System.Collections.Generic;
using System.Threading;
using System.Xml.Serialization;

namespace Epic.Training.Example
{
    /// <summary>
    /// Used to manage cheese serialization to/from an XML file
    /// </summary>
    [XmlRoot(Namespace = "Epic.Training.Example.CheeseList")]
    public class CheeseList
    {
        /// <summary>
        /// Used to manage access to cheese lists.
        /// </summary>
        [XmlIgnore]
        public static ReaderWriterLockSlim CheeseLock { get; private set; }

        /// <summary>
        /// Static constructor used to initialize the CheeseLock property
        /// </summary>
        static CheeseList()
        {
            CheeseLock = new ReaderWriterLockSlim();
        }

        /// <summary>
        /// Stores the next unique ID for cheeses added to the list
        /// </summary>
        [XmlAttribute]
        public int NextId { get; set; }

        /// <summary>
        /// A list of Cheeses
        /// </summary>
        [XmlElement("Cheese")]
        public List<Cheese> Cheeses { get; set; }

        /// <summary>
        /// Add or replace a cheese in this list.  If the Id property matches another cheese,
        /// the current cheese in the list is replaced with the new one.  If the Id is unique, the 
        /// cheese is added instead.  If the Id is -1, a uniqe Id is assigned and the cheese is added.
        /// ImagePathLarge is modified to match ImagePathSmall before the cheese is added to the list if
        /// ImagePathSmall isn't null.
        /// 
        /// If replacing an old cheese, ImagePathLarge and Small are copied over if those properties 
        /// are null or whitespace.
        /// </summary>
        /// <param name="newCheese">The new/modified cheese</param>
        /// <returns>Item number of the inserted cheese</returns>
        public void AddOrReplaceCheese(Cheese newCheese)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(newCheese.Name))
                {
                    throw new Exception("Cheese name cannot be null");
                }
                if (!String.IsNullOrWhiteSpace(newCheese.ImagePathSmall))
                {
                    string[] image = newCheese.ImagePathSmall.Split('-');
                    if (String.IsNullOrWhiteSpace(newCheese.ImagePathLarge) ||
                        newCheese.ImagePathLarge.Split('-')[0] != image[0])
                    {
                        // ImagePathSmall doesn't match ImagePathLarge.  Fix this issue now
                        newCheese.ImagePathLarge = image[0] + "-Full.png";
                    }
                }
                if (newCheese.Item == -1)
                {
                    // This cheese needs a new ID assigned
                    newCheese.Item = NextId++;
                }
                else
                {
                    for (int cIdx = 0; cIdx < Cheeses.Count; cIdx++)
                    {
                        Cheese aCheese = Cheeses[cIdx];
                        if (newCheese.Item == aCheese.Item)
                        {
                            Cheese oldCheese = Cheeses[cIdx];
                            // If new cheese doesn't have images, replace with old images
                            if (newCheese.ImagePathLarge == null)
                            {
                                newCheese.ImagePathLarge = oldCheese.ImagePathLarge;
                            }
                            if (newCheese.ImagePathSmall == null)
                            {
                                newCheese.ImagePathSmall = oldCheese.ImagePathSmall;
                            }
                            Cheeses[cIdx] = newCheese;
                            return;
                        }
                    }
                }
                // The new cheese has a unique item number.  Add to the list.
                Cheeses.Add(newCheese);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        /// <summary>
        /// Get a single cheese from a cheese list
        /// </summary>
        /// <param name="country">The country code</param>
        /// <param name="item">The cheese's item number</param>
        /// <returns>A cheese with matching item value, or null.</returns>
        public Cheese GetACheese(string country, int item)
        {
            foreach (Cheese c in Cheeses)
            {
                if (c.Item == item)
                {
                    return c;
                }
            }
            return null;
        }
    }
}
