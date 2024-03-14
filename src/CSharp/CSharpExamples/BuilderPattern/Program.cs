// See https://aka.ms/new-console-template for more information
using BuilderPattern;

Console.WriteLine("Hello, World!");

GiftCardBuilder giftCardBuilder = new GiftCardBuilder();
giftCardBuilder.AddNote("Hello");
giftCardBuilder.AddGlitter("Shiny");

GiftCard card = giftCardBuilder.GetCard();
