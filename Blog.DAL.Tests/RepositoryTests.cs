using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using Blog.DAL.Infrastructure;
using Blog.DAL.Model;
using Blog.DAL.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using TDD.DbTestHelpers.Yaml;
using TDD.DbTestHelpers.Core;
using System.Collections.Generic;

namespace Blog.DAL.Tests
{

    public class BlogFixtures : YamlDbFixture<BlogContext, BlogFixturesModel>
    {
        public BlogFixtures()
        {
            SetYamlFiles("posts.yml");
        }
    }

    public class BlogFixturesModel
    {
        public FixtureTable<Post> Posts { get; set; }
    }

    [TestClass]
    public class RepositoryTests
    {

        public class DbBaseTest<BlogFixtures>
        {

        }

        [TestMethod]
        public void GetAllPosts_ThreePostsInDb_ReturnThreePosts()
        {
            // arrange
            List<Post> posty = new List<Post>();
            List<Comment> komentarze = new List<Comment>();
            Setup(posty, komentarze);

            // act
            int ile = posty.Count();
            // assert
            Assert.AreEqual(3, ile);
        }

        [TestMethod]
        public void GetPostsByAutor2_WroteTwoPosts_ReturnCorrectPostNames()
        {
            // arrange
            List<Post> posty = new List<Post>();
            List<Comment> komentarze = new List<Comment>();
            Setup(posty, komentarze);

            // act
            string contents;
            contents = null;
            for (int i = 0; i < posty.Count; i++)
            {
                if (posty[i].Author == "autor 2")
                {
                    contents += posty[i].Content;
                    contents += ", ";
                }
            }
            // assert
            Assert.AreEqual(contents, "post 2, post 3, ");
        }

        [TestMethod]
        public void GetCommentsToPostOne_HasTwoComments_ReturnTwoComments()
        {
            // arrange
            List<Post> posty = new List<Post>();
            List<Comment> komentarze = new List<Comment>();
            Setup(posty, komentarze);

            // act
            int num = 0;
            for (int i = 0; i < komentarze.Count; i++)
            {
                if (komentarze[i].Post == 1)
                    num++;
            }
            // assert
            Assert.AreEqual(2, num);
        }

        [TestMethod]
        public void GetCommentsToPostTwo_HasOneComment_ReturnCorrectCommentContent()
        {
            // arrange
            List<Post> posty = new List<Post>();
            List<Comment> komentarze = new List<Comment>();
            Setup(posty, komentarze);

            // act
            var context = new BlogContext();
            string contents;
            contents = null;
            for (int i = 0; i < komentarze.Count; i++)
            {
                if (komentarze[i].Post == 2)
                {
                    contents += komentarze[i].Content;
                    contents += ", ";
                }
            }
            // assert
            Assert.AreEqual(contents, "komentarz do postu 2, ");
        }

        public void Setup(List<Post> posty, List<Comment> komentarze)
        {
            var post = new Post();

            post.Id = 1;
            post.Nr = 1;
            post.Author = "autor 1";
            post.Content = "post 1";
            posty.Add(post);

            var post2 = new Post();

            post2.Id = 2;
            post2.Nr = 2;
            post2.Author = "autor 2";
            post2.Content = "post 2";
            posty.Add(post2);

            var post3 = new Post();

            post3.Id = 3;
            post3.Nr = 3;
            post3.Author = "autor 2";
            post3.Content = "post 3";
            posty.Add(post3);

            var komentarz = new Comment();

            komentarz.Id = 1;
            komentarz.Post = 1;
            komentarz.Content = "komentarz do postu 1";
            komentarze.Add(komentarz);

            var komentarz2 = new Comment();

            komentarz2.Id = 2;
            komentarz2.Post = 1;
            komentarz2.Content = "drugi komentarz do postu 1";
            komentarze.Add(komentarz2);

            var komentarz3 = new Comment();

            komentarz3.Id = 3;
            komentarz3.Post = 2;
            komentarz3.Content = "komentarz do postu 2";
            komentarze.Add(komentarz3);
        }
    }
}
