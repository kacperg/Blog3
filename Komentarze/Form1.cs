using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.IO;
using System.Reflection;
using Blog.DAL.Infrastructure;
using Blog.DAL.Model;
using Blog.DAL.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace Komentarze
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            var context = new BlogContext();
            context.Database.CreateIfNotExists();
            var repository = new BlogRepository();

            var results = repository.GetAllPosts();

            List<long> _items = new List<long>();
            List<string> _items2 = new List<string>();

            foreach (var result in results)
            {
                _items.Add(result.Nr);
            }

            listBox1.DataSource = _items;

            var comments = context.Comments.ToList();
            long text = Convert.ToInt64(listBox1.GetItemText(listBox1.SelectedItem));
            foreach (var comment in comments)
            {
                if (comment.Post == text)
                _items2.Add(comment.Content);
            }

            foreach (var result in results)
            {
                if (result.Nr == text)
                {
                    textBox1.Text = result.Author;
                    textBox2.Text = result.Content;
                    break;
                }
            }

            listBox2.DataSource = _items2;
        }

        private void listBox1_SelectedIndexChanged_1(object sender, System.EventArgs e)
        {
            var context = new BlogContext();
            context.Database.CreateIfNotExists();
            var repository = new BlogRepository();

            var results = repository.GetAllPosts();

            List<string> _items2 = new List<string>();
            var comments = context.Comments.ToList();
            long text = Convert.ToInt64(listBox1.GetItemText(listBox1.SelectedItem));
            foreach (var comment in comments)
            {
                if (comment.Post == text)
                    _items2.Add(comment.Content);
            }

            foreach (var result in results)
            {
                if (result.Nr == text)
                {
                    textBox1.Text = result.Author;
                    textBox2.Text = result.Content;
                    textBox1.Update();
                    textBox2.Update();
                    break;
                }
            }

            listBox2.DataSource = _items2;
            listBox2.Update();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var context = new BlogContext();
            context.Database.CreateIfNotExists();
            var repository = new BlogRepository();
            long text = Convert.ToInt64(listBox1.GetItemText(listBox1.SelectedItem));

            context.Comments.Add(new Comment
            {
                Post = text,
                Content = textBox3.Text
            });
            context.SaveChanges();

            List<string> _items2 = new List<string>();
            var comments = context.Comments.ToList();
            foreach (var comment in comments)
            {
                if (comment.Post == text)
                    _items2.Add(comment.Content);
            }
            listBox2.DataSource = _items2;
            listBox2.Update();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var context = new BlogContext();
            context.Database.CreateIfNotExists();
            var repository = new BlogRepository();

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

            var results = repository.GetAllPosts();

            List<long> _items = new List<long>();
            List<string> _items2 = new List<string>();

            foreach (var result in results)
            {
                _items.Add(result.Nr);
            }

            listBox1.DataSource = _items;

            var comments = context.Comments.ToList();
            long text = Convert.ToInt64(listBox1.GetItemText(listBox1.SelectedItem));
            foreach (var comment in comments)
            {
                if (comment.Post == text)
                    _items2.Add(comment.Content);
            }

            foreach (var result in results)
            {
                if (result.Nr == text)
                {
                    textBox1.Text = result.Author;
                    textBox2.Text = result.Content;
                    break;
                }
            }

            listBox2.DataSource = _items2;
        }
    }
}
