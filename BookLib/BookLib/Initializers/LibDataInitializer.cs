using BookLib.Data;
using BookLib.Models.DBModels;
using System.Linq;

namespace BookLib.Initializers
{
    public class LibDataInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            if (!context.Category.Any())
            {
                context.Category.Add(
                    new Category
                    {
                        Name = "Художественная литература"
                    });
                context.SaveChanges();
            }

            int categoryId = context.Category.FirstOrDefault(c => c.Name == "Художественная литература").Id;

            if (!context.Genre.Any())
            {
                context.Genre.AddRange(
                    new Genre
                    {
                        Name = "Детективы, триллеры",
                        IdCategory = categoryId
                    },
                    new Genre
                    {
                        Name = "Фантастика, фэнтези",
                        IdCategory = categoryId
                    },
                    new Genre
                    {
                        Name = "Романы",
                        IdCategory = categoryId
                    }
               );

                context.SaveChanges();
            }

            if (!context.Author.Any())
            {
                context.Author.AddRange(
                    new Author
                    {
                        Name = "Ю Несбе"
                    },
                    new Author
                    {
                        Name = "Оливер Боуден"
                    },
                    new Author
                    {
                        Name = "Филис Кристина Каст"
                    },
                    new Author
                    {
                        Name = "Нина Лакур"
                    },
                    new Author
                    {
                        Name = "Анна Тодд"
                    }
                );

                context.SaveChanges();
            }

            if (!context.Publisher.Any())
            {
                context.Publisher.AddRange(
                    new Publisher
                    {
                        Name = "Иностранка"
                    },
                    new Publisher
                    {
                        Name = "Азбука"
                    },
                    new Publisher
                    {
                        Name = "Центрполиграф"
                    },
                    new Publisher
                    {
                        Name = "Popcorn Books"
                    },
                    new Publisher
                    {
                        Name = "Эксмо"
                    }
                );

                context.SaveChanges();
            }

            if (!context.Series.Any())
            {
                context.Series.AddRange(
                    new Series
                    {
                        Name = "Авторская серия Ю. Несбё"
                    }
                );

                context.SaveChanges();
            }

            if (!context.Book.Any())
            {
                int authorId1 = context.Author.FirstOrDefault(a => a.Name == "Ю Несбе").Id;
                int publisherId1 = context.Publisher.FirstOrDefault(a => a.Name == "Иностранка").Id;
                int seriesId1 = context.Series.FirstOrDefault(a => a.Name == "Авторская серия Ю. Несбё").Id;
                int genreId1 = context.Genre.FirstOrDefault(a => a.Name == "Детективы, триллеры").Id;

                int authorId2 = context.Author.FirstOrDefault(a => a.Name == "Оливер Боуден").Id;
                int publisherId2 = context.Publisher.FirstOrDefault(a => a.Name == "Азбука").Id;
                int genreId2 = context.Genre.FirstOrDefault(a => a.Name == "Фантастика, фэнтези").Id;

                int authorId3 = context.Author.FirstOrDefault(a => a.Name == "Филис Кристина Каст").Id;
                int publisherId3 = context.Publisher.FirstOrDefault(a => a.Name == "Центрполиграф").Id;

                int authorId4 = context.Author.FirstOrDefault(a => a.Name == "Нина Лакур").Id;
                int publisherId4 = context.Publisher.FirstOrDefault(a => a.Name == "Popcorn Books").Id;
                int genreId3 = context.Genre.FirstOrDefault(a => a.Name == "Романы").Id;

                int authorId5 = context.Author.FirstOrDefault(a => a.Name == "Анна Тодд").Id;
                int publisherId5 = context.Publisher.FirstOrDefault(a => a.Name == "Эксмо").Id;
                context.Book.AddRange(
                    new Book
                    {
                        Isbn = "978-5-389-13820-9",
                        Name = "Призрак",
                        Description = "После трехлетнего отсутствия бывший полицейский Харри Холе воз вращается в Норвегию, " +
                        "чтобы расследовать еще одно убийство. На этот раз им движут глубоко личные мотивы: обвиняемый - сын " +
                        "его прежней возлюбленной Ракели. Харри знал Олега еще ребенком и теперь готов разбиться в лепешку, " +
                        "чтобы доказать его невиновность. Поскольку убитый был наркодилером, Харри начинает поиски в этом направлении. " +
                        "В ходе своего неофициального расследования он узнает о существовании таинственного человека, заправляющего " +
                        "местной наркосетью. Его имени никто не знает. Он появляется из ниоткуда, как призрак, дает указания, казнит " +
                        "и милует, а затем вновь исчезает. Его помощники действуют жестко и убивают не задумываясь. Харри понимает, " +
                        @"что, только подобравшись к этому зловещему ""призраку"", он сумеет помочь Олегу...",
                        ReleaseYear = 2017,
                        IdAuthor = authorId1,
                        IdPublisher = publisherId1,
                        IdSeries = seriesId1,
                        IdCategory = categoryId,
                        IdGenre = genreId1
                    },
                    new Book
                    {
                        Isbn = "978-5-389-11463-0",
                        Name = "Снеговик",
                        Description = "Поистине в первом снеге есть что-то колдовское. Он не только сводит любовников, заглушает звуки, " +
                        "удлиняет тени, скрывает следы. Годами в Норвегии в тот день, когда выпадает первый снег, бесследно исчезают " +
                        "замужние женщины.На этот раз Харри Холе сталкивается с серийным убийцей на своей родной земле, и постепенно их " +
                        "противостояние приобретает личный характер. Преступник, которому газеты дали прозвище Снеговик, будто дразнит " +
                        "старшего инспектора, шаг за шагом подбираясь к его близким...",
                        ReleaseYear = 2016,
                        IdAuthor = authorId1,
                        IdPublisher = publisherId1,
                        IdSeries = seriesId1,
                        IdCategory = categoryId,
                        IdGenre = genreId1
                    },
                    new Book
                    {
                        Isbn = "978-5-389-12457-8",
                        Name = "Assassin's Creed. Черный флаг",
                        Description = "Начало 18-го века. Эдвард Кенуэй, дерзкий, самоуверенный сын фермера и торговца, с детских лет " +
                        "мечтает о дальних странствиях, о славе и богатстве. Однажды ферма его родителей подвергается нападению и сгорает " +
                        "дотла. Жизнь самого Эдварда в опасности, теперь юноша просто вынужден покинуть родные места. Достаточно скоро " +
                        "Эдвард Кенуэй становится грозным капером. Но за ним по пятам неотступно следуют алчность, честолюбие и предательство. " +
                        "И когда Кенуэй узнает о подлом заговоре, грозящем уничтожить все, что ему дорого, он не может побороть в себе желание " +
                        "отомстить врагам. Так он втягивается в многовековую битву между ассасинами и тамплиерами. Основой для книги послужила " +
                        "популярная компьютерная игра компании «Ubisoft». Впервые на русском языке!",
                        ReleaseYear = 2017,
                        IdAuthor = authorId2,
                        IdPublisher = publisherId2,
                        IdSeries = null,
                        IdCategory = categoryId,
                        IdGenre = genreId2
                    },
                    new Book
                    {
                        Isbn = "978-5-227-08098-1",
                        Name = "Богиня по выбору",
                        Description = "В жизни обычной учительницы из небольшого города в штате Оклахома происходит удивительное и невероятное " +
                        "событие - она внезапно оказывается в параллельном мире под названием Партолон. Здесь люди, двойники из ее привычной " +
                        "жизни, существуют по другим законам, здесь магия и волшебство считаются обычным делом, люди живут рядом с кентаврами, " +
                        "общаются с богами и духами, воюют с вампирами. Проведя несколько месяцев в Партолоне, Шеннон влюбляется, выходит замуж " +
                        "и только узнает, что ждет ребенка, как не менее странным образом возвращается в Оклахому. После череды трагических событий " +
                        "героиня узнает, что все произошедшее не было случайным, ее задача уничтожить дух зла, который способен разрушить не только " +
                        "ее жизнь, но и убить тех, кого она любила в обоих мирах.",
                        ReleaseYear = 2018,
                        IdAuthor = authorId3,
                        IdPublisher = publisherId3,
                        IdSeries = null,
                        IdCategory = categoryId,
                        IdGenre = genreId2
                    },
                    new Book
                    {
                        Isbn = "978-5-6040721-9-6",
                        Name = "Мы в порядке",
                        Description = "Марин бросила все и сбежала из родного города, не объяснив причин даже лучшей подруге Мейбл. Она поступает в " +
                        "Нью-Йоркский колледж в тысячах километров от дома и пытается начать новую жизнь. Однако, когда на новогодние каникулы к ней " +
                        "приезжает Мейбл, намереваясь возобновить общение, прошлое настигает Марин с новой силой, вынуждая взглянуть в лицо одиночеству " +
                        @"и страхам. ""Мы в порядке"" - роман об утраченной любви, принятии себя, скорби и семейных тайнах.",
                        ReleaseYear = 2019,
                        IdAuthor = authorId4,
                        IdPublisher = publisherId4,
                        IdSeries = null,
                        IdCategory = categoryId,
                        IdGenre = genreId3
                    },
                    new Book
                    {
                        Isbn = "978-5-699-78760-9",
                        Name = "После",
                        Description = "Поклонники трилогии Э Л Джеймс с восторгом встретили появление книг Анны Тодд, которые сама автор назвала " +
                        "«ванильной версией «Пятидесяти оттенков».Миллион читателей во всем мире следили за историей отношений Тесс и Хардина – примерной " +
                        "девочки и плохого парня. Тесс была прилежной ученицей и послушной дочерью, но после встречи с Хардином ее жизнь абсолютно " +
                        "изменилась. Оказалось, что есть на свете кое-что поважнее учебы и карьеры…",
                        ReleaseYear = 2015,
                        IdAuthor = authorId5,
                        IdPublisher = publisherId5,
                        IdSeries = null,
                        IdCategory = categoryId,
                        IdGenre = genreId3
                    }
                );

                context.SaveChanges();
            }

            if (!context.Availability.Any())
            {
                context.Book.ToList().ForEach(b => context.Availability.Add(
                    new Availability
                    {
                        TotalCount = 10,
                        FreeCount = 10,
                        OnHandsCount = 0,
                        ExpiredCount = 0,
                        IdBook = b.Id
                    }
                ));

                context.SaveChanges();
            }
        }
    }
}
