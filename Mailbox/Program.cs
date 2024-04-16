using System;
using System.Collections.Generic;

namespace Post
{
    class Box
    {
        private List<Mail> mails;
        private double maxSize;

        public Box(double maxSize)
        {
            this.maxSize = maxSize;
            mails = new List<Mail>();
        }

        public void addMail(Mail mail)
        {
            if (mails.Count < maxSize)
            {
                mails.Add(mail);
            }
            else
            {
                Console.WriteLine("The box is full. Cannot add more mails.");
            }
        }

        public double stamp()
        {
            double totalAmount = 0;
            foreach (Mail mail in mails)
            {
                totalAmount += mail.getStampAmount();
            }
            return totalAmount;
        }

        public void display()
        {
            foreach (Mail mail in mails)
            {
                if (mail.isValid())
                {
                    Console.WriteLine(mail);
                }
                else
                {
                    Console.WriteLine(mail + "\n(Invalid courier)");
                }
            }
        }

        public List<Mail> getInvalidMails()
        {
            List<Mail> invalidMails = new List<Mail>();
            foreach (Mail mail in mails)
            {
                if (!mail.isValid())
                {
                    invalidMails.Add(mail);
                }
            }
            return invalidMails;
        }
    }

    abstract class Mail
    {
        protected double weight;
        protected bool express;
        protected string destinationAddress;

        public Mail(double weight, bool express, string destinationAddress)
        {
            this.weight = weight;
            this.express = express;
            this.destinationAddress = destinationAddress;
        }

        public abstract double getStampAmount();

        public bool isValid()
        {
            return !string.IsNullOrEmpty(destinationAddress);
        }
    }

    class Lettre : Mail
    {
        private string format;

        public Lettre(double weight, bool express, string destinationAddress, string format) 
            : base(weight, express, destinationAddress)
        {
            this.format = format;
        }

        public override double getStampAmount()
        {
            double baseFare = format == "A4" ? 2.50 : 3.50;
            double amount = express ? 2 * (baseFare + 1.0 * (weight / 1000)) : baseFare + 1.0 * (weight / 1000);
            return amount;
        }

        public override string ToString()
        {
            return "Letter\n" + 
                   "Weight: " + weight + " grams\n" + 
                   "Express: " + express + "\n" + 
                   "Destination: " + destinationAddress + "\n" +
                   "Price: $" + getStampAmount() + "\n" +
                   "Format: " + format;
        }
    }

    class Advertisement : Mail
    {
        public Advertisement(double weight, bool express, string destinationAddress) 
            : base(weight, express, destinationAddress)
        {
        }

        public override double getStampAmount()
        {
            return express ? 2 * (5.0 * (weight / 1000)) : 5.0 * (weight / 1000);
        }

        public override string ToString()
        {
            return "Advertisement\n" + 
                   "Weight: " + weight + " grams\n" + 
                   "Express: " + express + "\n" + 
                   "Destination: " + destinationAddress + "\n" +
                   "Price: $" + getStampAmount();
        }
    }

    class Parcel : Mail
    {
        private double volume;

        public Parcel(double weight, bool express, string destinationAddress, double volume) 
            : base(weight, express, destinationAddress)
        {
            this.volume = volume;
        }

        public override double getStampAmount()
        {
            double amount = express ? 2 * (0.25 * volume + weight / 1000) : 0.25 * volume + weight / 1000;
            return amount;
        }

        public override string ToString()
        {
            return "Parcel\n" + 
                   "Weight: " + weight + " grams\n" + 
                   "Express: " + express + "\n" + 
                   "Destination: " + destinationAddress + "\n" +
                   "Price: $" + getStampAmount() + "\n" +
                   "Volume: " + volume + " liters";
        }
    }

    class Post
    {
        public static void Main(string[] args)
        {
           
            Box box = new Box(30);

            Lettre lettre1 = new Lettre(200, true, "Chemin des Acacias 28, 1009 Pully", "A3");
            Lettre lettre2 = new Lettre(800, false, "", "A4"); 

            Advertisement adv1 = new Advertisement(1500, true, "Les Moilles 13A, 1913 Saillon");
            Advertisement adv2 = new Advertisement(3000, false, ""); 

            Parcel parcel1 = new Parcel(5000, true, "Grand rue 18, 1950 Sion", 30);
            Parcel parcel2 = new Parcel(3000, true, "Chemin des fleurs 48, 2800 Delemont", 70); 

            box.addMail(lettre1);
            box.addMail(lettre2);
            box.addMail(adv1);
            box.addMail(adv2);
            box.addMail(parcel1);
            box.addMail(parcel2);

            Console.WriteLine("The total amount of postage is $" + box.stamp());
            box.display();

            List<Mail> invalidMails = box.getInvalidMails();
            Console.WriteLine("The box contains " + invalidMails.Count + " invalid mails");
        }
    }
}
