using Blog.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Controllers
{
    public class BlogController : Controller
    {
        // GET: Blog
        private PassageDBContext Passagesdb = new PassageDBContext();
        private BoardsDBContext Boardsdb = new BoardsDBContext();
        private UserDBContext Usersdb = new UserDBContext();
        public ActionResult Index(string name)//主页
        {
            if (name != null)
                ViewBag.name = name;
            else
            {
                if (Session["Author"] != null)
                {
                    ViewBag.name = Session["Author"];
                    name = (string)Session["Author"];
                }
            }
            return View();
        }

        public ActionResult Edit(string name)//编辑页面
        {
            if (name != null)
                ViewBag.name = name;
            else
            {
                if (Session["Author"] != null)
                {
                    ViewBag.name = Session["Author"];
                    name = (string)Session["Author"];
                }
            }
            List<Passage> li = new List<Passage>();
            if (name != null && name.Equals(Session["Author"]))
            {
                List<Passage> l = Passagesdb.Passages.ToList();
                for (int i = 0; i < l.Count; i++)
                {
                    if (l[i].Author.Equals(name))
                        li.Add(l[i]);
                }
            }
            return View(li);
        }
        public ActionResult Contain(string name, int page = 0)//文章内容页面
        {
            if (name != null)
                ViewBag.name = name;
            else
            {
                if (Session["Author"] != null)
                {
                    ViewBag.name = Session["Author"];
                    name = (string)Session["Author"];
                }
            }
            int setPage = 5;
            List<Passage> l = Passagesdb.Passages.ToList();
            List<Passage> ls = new List<Passage>();
            for (int i = 0; i < l.Count; i++)
            {
                if (l[i].Author.Equals(name))
                    ls.Add(l[i]);
            }
            List<Passage> li = new List<Passage>();
            int init = setPage * page;
            for (int i = init; i < ls.Count && i < init + setPage; i++)
            {
                li.Add(ls[i]);
            }
            return View(li);
        }

        public ActionResult Board(string name)//留言板
        {
            if (name != null)
                ViewBag.name = name;
            else
            {
                if (Session["Author"] != null)
                {
                    ViewBag.name = Session["Author"];
                    name = (string)Session["Author"];
                }
            }
            List<Boards> l = Boardsdb.Boards.ToList();
            List<Boards> li = new List<Boards>();
            for (int i = 0; i < l.Count; i++)
            {
                if (l[i].Author.Equals(name))
                {
                    li.Add(l[i]);
                }
            }
            return View(li);
        }
        public void DeleteBoard(string name,int ID)
        {
            if (name != null)
                ViewBag.name = name;
            else
            {
                if (Session["Author"] != null)
                {
                    ViewBag.name = Session["Author"];
                    name = (string)Session["Author"];
                }
            }
            Boards b=Boardsdb.Boards.Find(ID);
            Boardsdb.Boards.Remove(b);
            Boardsdb.SaveChanges();
            Response.Redirect("/Blog/Board?name="+name);
        }
        public ActionResult AddPas(string name)//添加文章页面
        {
            if (name != null)
                ViewBag.name = name;
            else
            {
                if (Session["Author"] != null)
                {
                    ViewBag.name = Session["Author"];
                    name = (string)Session["Author"];
                }
            }
            return View();
        }
        [HttpPost]
        public ActionResult CommitNewPas(string name, Passage passage)//提交新文章
        {
            if (name != null)
                ViewBag.name = name;
            else
            {
                if (Session["Author"] != null)
                {
                    ViewBag.name = Session["Author"];
                    name = (string)Session["Author"];
                }
            }
            passage.OutDate = DateTime.Now.Date;
            passage.Text = passage.Text.Trim();
            passage.Author = name;
            passage.Title = passage.Title.Trim();
            passage.Sort = passage.Sort.Trim();
            Passagesdb.Passages.Add(passage);
            Passagesdb.SaveChanges();
            return RedirectToAction("Contain");
        }
        public ActionResult DeletePas(string name, int id)//删除文章
        {
            if (name != null)
                ViewBag.name = name;
            else
            {
                if (Session["Author"] != null)
                {
                    ViewBag.name = Session["Author"];
                    name = (string)Session["Author"];
                }
            }
            Passage p = Passagesdb.Passages.Find(id);
            Passagesdb.Passages.Remove(p);
            Passagesdb.SaveChanges();
            return RedirectToAction("Edit");
        }
        [HttpPost]
        public ActionResult AdjPas(string name, Passage passage)//向数据库提交编辑文章
        {
            if (name != null)
                ViewBag.name = name;
            else
            {
                if (Session["Author"] != null)
                {
                    ViewBag.name = Session["Author"];
                    name = (string)Session["Author"];
                }
            }
            var result = from p in Passagesdb.Passages
                         where p.ID == passage.ID
                         select p;
            foreach (var p in result)
            {
                p.Sort = passage.Sort.Trim();
                p.Text = passage.Text.Trim();
                p.Title = passage.Title.Trim();
                p.OutDate = DateTime.Now.Date;
            }
            Passagesdb.SaveChanges();
            return RedirectToAction("Contain");
        }
        public ActionResult ShowPas(string name, int id)//显示文章
        {
            if (name != null)
                ViewBag.name = name;
            else
            {
                if (Session["Author"] != null)
                {
                    ViewBag.name = Session["Author"];
                    name = (string)Session["Author"];
                }
            }
            Passage p = Passagesdb.Passages.Find(id);
            return View(p);
        }
        public ActionResult EditPas(string name, int id)//编辑文章页面
        {
            if (name != null)
                ViewBag.name = name;
            else
            {
                if (Session["Author"] != null)
                {
                    ViewBag.name = Session["Author"];
                    name = (string)Session["Author"];
                }
            }
            Passage p = Passagesdb.Passages.Find(id);
            return View(p);
        }
        [HttpPost]
        public ActionResult CommitEdit(string name, Passage passage)//提交编辑文章
        {
            if (name != null)
                ViewBag.name = name;
            else
            {
                if (Session["Author"] != null)
                {
                    ViewBag.name = Session["Author"];
                    name = (string)Session["Author"];
                }
            }
            Passage p = Passagesdb.Passages.Find(passage.ID);
            p.Sort = passage.Sort.Trim();
            p.Text = passage.Text.Trim();
            p.Author = (string)Session["Author"];
            p.Title = passage.Title.Trim();
            p.OutDate = DateTime.Now.Date;
            Passagesdb.Entry(p).State = EntityState.Modified;
            Passagesdb.SaveChanges();
            return RedirectToAction("Contain");
        }
        public void AddBoard(string name, Boards boards)//添加留言板
        {
            if (name != null)
                ViewBag.name = name;
            else
            {
                if (Session["Author"] != null)
                {
                    ViewBag.name = Session["Author"];
                    name = (string)Session["Author"];
                }
            }
            boards.UpDate = DateTime.Now.Date;
            boards.Author = name;
            if (Session["Author"] == null)
            {
                boards.BeLong = "游客";
            }
            else
            {
                boards.BeLong = (string)Session["Author"];
            }
            Boardsdb.Boards.Add(boards);
            Boardsdb.SaveChanges();
            Response.Redirect("/Blog/Board?name="+name);
        }
        public JsonResult GetLi(string name)//根据关键字查询
        {
            if (name != null)
                ViewBag.name = name;
            else
            {
                if (Session["Author"] != null)
                {
                    ViewBag.name = Session["Author"];
                    name = (string)Session["Author"];
                }
            }
            List<Passage> l = Passagesdb.Passages.ToList();
            List<string> ls = new List<string>();
            for (int i = 0; i < l.Count; i++)
            {
                if (l[i].Author.Equals(name))
                {
                    ls.Add(l[i].Sort);
                }
            }
            List<string> li = new List<string>();
            Dictionary<string, int> d = new Dictionary<string, int>();
            for (int i = 0; i < ls.Count; i++)
            {
                if (!d.ContainsKey(ls[i]))
                {
                    li.Add(ls[i]);
                    d.Add(ls[i], 1);
                }
            }
            return Json(li);
        }
        public ActionResult Filter(string name, string sort, string title)//获得查询结果
        {
            if (name != null)
                ViewBag.name = name;
            else
            {
                if (Session["Author"] != null)
                {
                    ViewBag.name = Session["Author"];
                    name = (string)Session["Author"];
                }
            }
            if (sort != null)
                sort = sort.Trim();
            if (title != null)
                title = title.Trim();
            List<Passage> l = Passagesdb.Passages.ToList();
            List<Passage> ls = new List<Passage>();
            for (int i = 0; i < l.Count; i++)
            {
                if (l[i].Author.Equals(name))
                {
                    ls.Add(l[i]);
                }
            }
            List<Passage> li = new List<Passage>();
            if (sort == null || "".Equals(sort))
            {
                if (title == null || "".Equals(title))
                {
                    return View(ls);
                }
                else
                {
                    for (int i = 0; i < ls.Count; i++)
                    {
                        if (title.Equals(ls[i].Title))
                            li.Add(ls[i]);
                    }
                }
            }
            else
            {
                if (title == null || "".Equals(title))
                {
                    for (int i = 0; i < ls.Count; i++)
                    {
                        if (sort.Equals(ls[i].Sort))
                            li.Add(ls[i]);
                    }
                }
                else
                {
                    for (int i = 0; i < ls.Count; i++)
                    {
                        if (sort.Equals(ls[i].Sort) && title.Equals(ls[i].Title))
                            li.Add(ls[i]);
                    }
                }
            }
            return View(li);
        }
        public JsonResult GetPage(string name)//获得分页个数
        {
            if (name != null)
                ViewBag.name = name;
            else
            {
                if (Session["Author"] != null)
                {
                    ViewBag.name = Session["Author"];
                    name = (string)Session["Author"];
                }
            }
            List<Passage> l = Passagesdb.Passages.ToList();
            int size = 0;
            for (int i = 0; i < l.Count; i++)
            {
                if (l[i].Author.Equals(name))
                    size++;
            }
            size = size % 5 == 0 ? size / 5 : size / 5 + 1;
            return Json(size);
        }
        public ActionResult Regist()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RegistIn(User user)//注册
        {
            user.Name = user.Name.Trim();
            user.Pwd = user.Pwd.Trim();
            List<User> l = Usersdb.Users.ToList();
            for (int i = 0; i < l.Count; i++)
            {
                if (l[i].Name.Equals(user.Name))
                {
                    return Json("该账号已被占用!");
                }
            }
            Usersdb.Users.Add(user);
            Usersdb.SaveChanges();
            Session["Author"] = user.Name;
            return Json("注册成功!");
        }
        [HttpPost]
        public ActionResult Login(User user)
        {
            List<User> l = Usersdb.Users.ToList();
            for (int i = 0; i < l.Count; i++)
            {
                if (l[i].Name.Equals(user.Name) && l[i].Pwd.Equals(user.Pwd))
                {
                    Session["Author"] = user.Name;
                    return Json("登录成功!");
                }
            }
            return Json("账号或密码错误!");
        }
        public ActionResult AdjPwd()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UpdatePwd(User user, string npwd)
        {
            npwd = npwd.Trim();
            List<User> l = Usersdb.Users.ToList();
            for (int i = 0; i < l.Count; i++)
            {
                if (l[i].Name.Equals(user.Name) && l[i].Pwd.Equals(user.Pwd))
                {
                    User u = Usersdb.Users.Find(l[i].ID);
                    u.Pwd = npwd;
                    Usersdb.Entry(u).State = EntityState.Modified;
                    Usersdb.SaveChanges();
                    return Json("密码修改成功!");
                }
            }
            return Json("账号或密码错误!");
        }
        public ActionResult Ground(string name, int page = 0)
        {
            if (name != null)
                ViewBag.name = name;
            else
            {
                if (Session["Author"] != null)
                {
                    ViewBag.name = Session["Author"];
                    name = (string)Session["Author"];
                }
            }
            int setPage = 5;
            List<Passage> l = Passagesdb.Passages.ToList();
            List<Passage> li = new List<Passage>();
            int init = setPage * page;
            for (int i = init; i < l.Count && i < init + setPage; i++)
            {
                li.Add(l[i]);
            }
            return View(li);
        }
        public ActionResult GetGroundPage(string name)
        {
            if (name != null)
                ViewBag.name = name;
            else
            {
                if (Session["Author"] != null)
                {
                    ViewBag.name = Session["Author"];
                    name = (string)Session["Author"];
                }
            }
            int size = Passagesdb.Passages.ToList().Count;
            size = size % 5 == 0 ? size / 5 : size / 5 + 1;
            return Json(size);
        }
    }
}