using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crm.Models;
using System.Linq.Expressions;
using System.Data.Entity.Infrastructure;
using System.Reflection;

namespace Crm.DAL
{
    public class PersonService
    {
        public PersonService()
        {
            //调用示例
            //DelByCondition(p => p.TableId == 44);

            //Person pModify = new Person()
            //{
            //    TableId = 44,
            //    PersonName = "修改后的名字",
            //    WebUserName = "xghdmz"
            //};
            //Modify(pModify, new string[] { "PersonName", "WebUserName" });

            //List<Person> ps = this.GetListByCondition(p => p.PersonName.Contains("潘") && p.WebUserName=="panshuaiyang" ).ToList();

            //List<Person> ps2 = this.GetListByCondition(p => p.PersonName.Contains("潘") && p.WebUserName == "panshuaiyang",p2=>p2.TableId).ToList();

            List<Person> ps3 = this.GetPageList(1, 5, p => p.TableId, p2 => p2.DepId == 2).ToList();

        }
        


        /// <summary>
        /// EF的数据上下文
        /// </summary>
        CRMDatabaseEntities dbCRM = new CRMDatabaseEntities();

        #region 新增实体 +int Add(Person p)
        /// <summary>
        /// 新增实体
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public int Add(Person p)
        {
            try
            {
                dbCRM.Person.Add(p);
                //保存成功以后会把自增的id和影响的行数返回给p。
                return dbCRM.SaveChanges();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 根据id删除数据 +int DelById(int pNo)
        /// <summary>
        /// 根据id删除数据
        /// </summary>
        /// <param name="pNo"></param>
        /// <returns></returns>
        public int DelById(int pNo)
        {
            try
            {
                //创建对象
                Person pDel = new Person()
                {
                    TableId = pNo
                };
                dbCRM.Person.Attach(pDel);
                dbCRM.Person.Remove(pDel);
                return dbCRM.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion

        #region 根据条件删除数据 +int DelByCondition(Expression<Func<Person, bool>> delWhere)
        /// <summary>
        /// 根据条件删除数据
        /// </summary>
        /// <param name="delWhere"></param>
        /// <returns></returns>
        public int DelByCondition(Expression<Func<Person, bool>> delWhere)
        {
            try
            {
                List<Person> pDels = dbCRM.Person.Where(delWhere).ToList();
                pDels.ForEach(
                    p => dbCRM.Person.Remove(p)
                    );
                return dbCRM.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        #endregion

        #region 修改 +int Modify(Person p, params string[] proNames)
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="p">要修改的对象</param>
        /// <param name="proNames">要修改的对象名称</param>
        /// <returns></returns>
        public int Modify(Person p, params string[] proNames)
        {
            try
            {
                DbEntityEntry entry = dbCRM.Entry<Person>(p);
                entry.State = System.Data.EntityState.Unchanged;
                foreach (string proName in proNames)
                {
                    entry.Property(proName).IsModified = true;
                }
                return dbCRM.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 根据条件检索集合中的数据+List<Person> GetListByCondition(Expression<Func<Person, bool>> whereLamada)
        /// <summary>
        /// 根据条件检索集合中的数据
        /// </summary>
        /// <param name="whereLamada"></param>
        /// <returns></returns>
        public List<Person> GetListByCondition(Expression<Func<Person, bool>> whereLamada)
        {
            return dbCRM.Person.Where(whereLamada).ToList();
        }
        #endregion

        #region 根据条件检索集合中的数据带排序功能+List<Person> GetListByCondition<Tkey> 
        /// <summary>
        /// 根据条件检索集合中的数据带排序功能
        /// </summary>
        /// <param name="whereLamada"></param>
        /// <returns></returns>
        public List<Person> GetListByCondition<Tkey>(Expression<Func<Person, bool>> whereLamada,Expression<Func<Person,Tkey>> orderLamada)
        {//Tkey 推断 ，使用的时候指定其类型，调用的时候可以忽略
            return dbCRM.Person.Where(whereLamada).OrderBy(orderLamada).ToList();
        }
        #endregion

        #region 按照查询条件和排序分页查询数据
        /// <summary>
        /// 按照查询条件和排序分页查询数据
        /// </summary>
        /// <typeparam name="Tkey">排序字段类型</typeparam>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="orderLamada">排序条件</param>
        /// <param name="whereLamada">查询条件</param>
        /// <returns></returns>
        public List<Person> GetPageList<Tkey>(int pageIndex, int pageSize, Expression<Func<Person, Tkey>> orderLamada, Expression<Func<Person, bool>> whereLamada)
        {
            try
            {
                return dbCRM.Person.Where(whereLamada).OrderBy(orderLamada).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        } 
        #endregion


        /// <summary>
        /// 批量修改
        /// </summary>
        /// <param name="modelMProps">要修改的列，以及修改后的值</param>
        /// <param name="wherelambda"></param>
        /// <param name="modifiedProNames"></param>
        /// <returns></returns>
        public int Modify(Person modelMProps,Expression<Func<Person,bool>> wherelambda,params string[] modifiedProNames)
        {
            try
            {
                //1.查询要修改的数据
                List<Person> listModifing = dbCRM.Person.Where(wherelambda).ToList();
                //2.获取实体类对象
                Type t = typeof(Person);
                //3.获取实体类所有的公有属性
                List<PropertyInfo> proInfos = t.GetProperties(BindingFlags.Instance | BindingFlags.Public).ToList();
                //4.创建一个实体属性 字典集合
                Dictionary<string, PropertyInfo> dictPros = new Dictionary<string, PropertyInfo>();
                //5.将实体属性中药修改的属性，添加到集合中 键：属性名 值：属性对象
                proInfos.ForEach(p => {
                    if(modifiedProNames.Contains(p.Name))
                    {
                        dictPros.Add(p.Name, p);
                    }
                });
                //6.循环要修改的属性名
                foreach (string proName in modifiedProNames)
                {
                    //判断要修改的属性名是否在实体类的属性集合中
                    if(dictPros.ContainsKey(proName))
                    {
                        //如果存在，则取出要修改的属性对象
                        PropertyInfo proInfo = dictPros[proName];
                        //取出要修改的值
                        object newValue = proInfo.GetValue(modelMProps, null);
                        //批量设置要修改的对象的属性
                        foreach (Person p in listModifing)
                        {
                            //为要修改的对象，要修改的属性，设置新的值
                            proInfo.SetValue(p, newValue, null);
                        }

                    }
                }
                return dbCRM.SaveChanges();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }

}
