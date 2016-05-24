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
        public void OneShouldBeOne()
        {
            // arrange
            // act
            int i = 1;
            // assert
            Assert.AreEqual(1, i);
        }

        /*
        [TestMethod]
        public void GetAllPost_ThreePostsInDb_ReturnThreePosts()
        {
            // arrange
            var repository = new BlogRepository();
            Setup();

            // act
            var result = repository.GetAllPosts();
            // assert
            Assert.AreEqual(3, result.Count());
        }

        [TestMethod]
        public void GetPostsByAutor2_WroteTwoPosts_ReturnCorrectPostNames()
        {
            // arrange
            var repository = new BlogRepository();
            Setup();

            // act
            var results = repository.GetAllPosts();
            string contents;
            contents = null;
            foreach(var result in results)
            {
                if (result.Author == "autor 2")
                {
                    contents += result.Content;
                    contents += ", ";
                }
            }
            // assert
            Assert.AreEqual(contents,"post 2, post 3, ");
        }

        [TestMethod]
        public void GetCommentsToPostOne_HasTwoComments_ReturnTwoComments()
        {
            // arrange
            var repository = new BlogRepository();
            Setup();

            // act
            var context = new BlogContext();
            var comments = context.Comments.ToList();
            int num = 0;
            foreach (var comment in comments)
            {
                if (comment.Post == 1)
                    num++;
            }
            // assert
            Assert.AreEqual(2, num);
        }

        [TestMethod]
        public void GetCommentsToPostTwo_HasOneComment_ReturnCorrectCommentContent()
        {
            // arrange
            var repository = new BlogRepository();
            Setup();

            // act
            var context = new BlogContext();
            var comments = context.Comments.ToList();
            string contents;
            contents = null;
            foreach (var comment in comments)
            {
                if (comment.Post == 2)
                {
                    contents += comment.Content;
                    contents += ", ";
                }
            }
            // assert
            Assert.AreEqual(contents, "komentarz do postu 2, ");
        }*/

        public void Setup()
        {
            var context = new BlogContext();
            context.Database.CreateIfNotExists();
            context.Posts.ToList().ForEach(x => context.Posts.Remove(x));
            context.Posts.Add(new Post
            {
                Nr = 1,
                Author = "autor 1",
                Content = "post 1"
            });

            context.Posts.Add(new Post
            {
                Nr = 2,
                Author = "autor 2",
                Content = "post 2"
            });

            context.Posts.Add(new Post
            {
                Nr = 3,
                Author = "autor 2",
                Content = "post 3"
            });

            context.Comments.ToList().ForEach(x => context.Comments.Remove(x));

            context.Comments.Add(new Comment
            {
                Post = 1,
                Content = "komentarz do postu 1"
            });

            context.Comments.Add(new Comment
            {
                Post = 1,
                Content = "drugi komentarz do postu 1"
            });

            context.Comments.Add(new Comment
            {
                Post = 2,
                Content = "komentarz do postu 2"
            });

            context.SaveChanges();
        }
    }
}
