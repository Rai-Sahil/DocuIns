using System;
using System.Collections.Generic;
using System.Linq;

class Program
{

    public static string ClassifyText(List<string> texts, List<string> labels, string new_text)
    {

        // Tokenize the new text
        var tokens = new_text.Split(new[] { ' ', '.', ',', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);

        // Calculate class probabilities
        var classProbabilities = new Dictionary<string, double>();
        var totalDocs = labels.Count;

        foreach (var label in labels.Distinct())
        {
            var documentsWithLabel = labels.Count(l => l == label);
            classProbabilities[label] = (double)documentsWithLabel / totalDocs;
        }

        // Calculate conditional probabilities
        var wordCounts = new Dictionary<string, Dictionary<string, int>>();
        var uniqueWords = texts.SelectMany(text => text.Split(new[] { ' ', '.', ',', '!', '?' }, StringSplitOptions.RemoveEmptyEntries)).Distinct();

        foreach (var label in labels.Distinct())
        {
            wordCounts[label] = new Dictionary<string, int>();
        }

        foreach (var i in Enumerable.Range(0, texts.Count))
        {
            var words = texts[i].Split(new[] { ' ', '.', ',', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);
            var label = labels[i];

            foreach (var word in words)
            {
                if (!wordCounts[label].ContainsKey(word))
                {
                    wordCounts[label][word] = 0;
                }

                wordCounts[label][word]++;
            }
        }

        // Calculate Naive Bayes scores for all labels
        var scores = new Dictionary<string, double>();
        foreach (var label in classProbabilities.Keys)
        {
            scores[label] = Math.Log(classProbabilities[label]);
            foreach (var word in tokens)
            {
                if (wordCounts[label].ContainsKey(word))
                {
                    scores[label] += Math.Log((double)(wordCounts[label][word] + 1) / (wordCounts[label].Values.Sum() + uniqueWords.Count()));
                }
            }
        }

        // Find the label with the highest score
        var predictedLabels = scores.OrderBy(x => x.Value).Select(x => x.Key).ToList();

        Console.WriteLine("The input text is: " + new_text);
        Console.WriteLine("Most relevant tag comes first: " + string.Join(", ", predictedLabels));
        Console.WriteLine("Predicted Label: " + predictedLabels.First());

        return predictedLabels.First();
    }

    static void Main(string[] args)
    {
        var texts = new List<string>
        {
        // Technology
        "The latest smartphone features and innovations.",
        "Programming languages and software development trends.",
        "Machine learning and artificial intelligence advancements.",
        "Cloud computing and the future of technology.",
        "Tech giants announce new products and services.",
        "Cutting-edge technology shaping the future.",
        "IoT and smart devices revolutionizing our lives.",
        "Blockchain technology and its applications.",
        "Cybersecurity threats and data protection.",
        "AI-driven solutions for business and industry.",
        "Innovations in renewable energy and sustainability.",
        "Advancements in autonomous vehicles and AI-driven cars.",
        "Virtual reality and augmented reality experiences.",
        "The impact of 5G technology on connectivity.",
        "Robotic automation and industry 4.0.",
        "Big data analytics and business insights.",
        "Tech startups and entrepreneurship in Silicon Valley.",
        "Space exploration and future missions to Mars.",
        "The role of AI in healthcare and medical breakthroughs.",
        "Quantum computing and its potential applications.",
        "Future trends in software development and coding.",
        
        // Sports
        "Basketball game: a thrilling showdown.",
        "Football season kicks off with excitement.",
        "Baseball team clinches the championship title.",
        "Tennis tournament finals: intense matches.",
        "Olympic athletes break world records.",
        "Soccer World Cup: the greatest football spectacle.",
        "Golf champions tee off at major tournaments.",
        "Athletic achievements in the world of sports.",
        "Epic rivalries in sports history.",
        "Extreme sports and adrenaline-pumping adventures.",
        "Paralympic athletes' inspiring stories of triumph.",
        "Women in sports: breaking barriers and records.",
        "Sports legends and their enduring legacies.",
        "The science of training and peak sports performance.",
        "Motorsport: speed and precision on the racetrack.",
        "Sports broadcasting and the fan experience.",
        "Sports equipment and innovations in gear.",
        "Sports psychology and mental toughness.",
        "Sports nutrition and diet for peak athletes.",
        "The history of the Olympic Games.",
        "The role of sports in promoting fitness and well-being.",
        
        // Food
        "Delicious cookie recipes for your sweet tooth.",
        "Healthy eating tips for a balanced diet.",
        "Exploring gourmet cuisines from around the world.",
        "Farm-to-table dining and locally-sourced ingredients.",
        "Mastering the art of French pastry baking.",
        "Vegetarian and vegan cooking for a sustainable lifestyle.",
        "Barbecue and grilling: the art of outdoor cooking.",
        "Food and wine pairings for culinary perfection.",
        "The joys of home cooking and family recipes.",
        "Global street food and culinary adventures.",
        "Baking and cake decorating: a sweet journey.",
        "Celebrity chefs and their culinary empires.",
        "The culture of food festivals and foodie gatherings.",
        "Sustainable farming and the future of food production.",
        "Food preservation and pickling techniques.",
        "Food history and the origins of culinary traditions.",
        "Spices and herbs: the essence of flavor.",
        "Brewing and craft beer culture.",
        "The art of making pasta from scratch.",
        "Exotic fruits and their unique flavors.",
        "Food documentaries and the culinary world.",
        
        // Science
        "Space exploration and missions to Mars.",
        "Breakthroughs in cancer research and treatment.",
        "Climate change impacts on the environment.",
        "Discovering new species in the Amazon rainforest.",
        "Quantum physics and the mysteries of the universe.",
        "Nanotechnology and its applications in science.",
        "Genetic engineering and the future of biotechnology.",
        "Astronomy and the wonders of the cosmos.",
        "Neuroscience and the study of the human brain.",
        "The quest for clean and renewable energy sources.",
        "The periodic table and the elements of chemistry.",
        "Environmental conservation and biodiversity.",
        "Evolutionary biology and the tree of life.",
        "Psychology and the exploration of human behavior.",
        "The physics of the universe and cosmology.",
        "Marine biology and the mysteries of the deep sea.",
        "Artificial intelligence and machine learning.",
        "Medical breakthroughs and cutting-edge treatments.",
        "Mathematics and the beauty of numbers.",
        "Geology and the Earth's geological history.",
        
        // Travel
        "Travel tips for your dream vacation destinations.",
        "Exploring historical landmarks and cultural heritage.",
        "Adventure travel in the wilderness and jungles.",
        "Cruises to exotic and remote island destinations.",
        "Off-the-beaten-path travel to hidden gems.",
        "Road trips and scenic drives through picturesque landscapes.",
        "Backpacking and budget travel adventures.",
        "Luxury travel and opulent destinations.",
        "Travel photography and capturing the world's beauty.",
        "Hiking and trekking in the great outdoors.",
        "Exploring ancient ruins and archaeological sites.",
        "Safari adventures and wildlife encounters.",
        "Sustainable travel and eco-friendly tourism.",
        "Culinary tourism and food adventures around the globe.",
        "Island hopping and beach getaways.",
        "Cultural festivals and celebrations worldwide.",
        "Solo travel and the art of self-discovery.",
        "RV and camper van travel for nomadic lifestyles.",
        "Train journeys and scenic railway routes.",
        "Winter sports and snowy getaways.",
        "Traveling with kids and family-friendly vacations."
    };

        var labels = new List<string>
        {
            "technology",
            "technology",
            "technology",
            "technology",
            "technology",
            "technology",
            "technology",
            "technology",
            "technology",
            "technology",
            "technology",
            "technology",
            "technology",
            "technology",
            "technology",
            "technology",
            "technology",
            "technology",
            "technology",
            "technology",
            "sport",
            "sport",
            "sport",
            "sport",
            "sport",
            "sport",
            "sport",
            "sport",
            "sport",
            "sport",
            "sport",
            "sport",
            "sport",
            "sport",
            "sport",
            "sport",
            "sport",
            "sport",
            "sport",
            "sport",
            "food",
            "food",
            "food",
            "food",
            "food",
            "food",
            "food",
            "food",
            "food",
            "food",
            "food",
            "food",
            "food",
            "food",
            "food",
            "food",
            "food",
            "food",
            "food",
            "food",
            "science",
            "science",
            "science",
            "science",
            "science",
            "science",
            "science",
            "science",
            "science",
            "science",
            "science",
            "science",
            "science",
            "science",
            "science",
            "science",
            "science",
            "science",
            "science",
            "science",
            "travel",
            "travel",
            "travel",
            "travel",
            "travel",
            "travel",
            "travel",
            "travel",
            "travel",
            "travel",
            "travel",
            "travel",
            "travel",
            "travel",
            "travel",
            "travel",
            "travel",
            "travel",
            "travel",
            "travel",
        };

        var new_text = "Exploring new food recipes in the kitchen.";
        ClassifyText(texts, labels, new_text);
    }
}
