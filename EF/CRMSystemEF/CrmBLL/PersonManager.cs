using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crm.Models;
using System.Linq.Expressions;
using Crm.DAL;

namespace Crm.BLL
{
    public class PersonManager
    {
        /// <summary>
        /// EF的数据上下文
        /// </summary>
        PersonService ps = new PersonService();

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
                return ps.Add(p);
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
                return ps.DelById(pNo);
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
                return ps.DelByCondition(delWhere);
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
                return ps.Modify(p, proNames);
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
            return ps.GetListByCondition(whereLamada);
        }
        #endregion

        #region 根据条件检索集合中的数据带排序功能+List<Person> GetListByCondition<Tkey> 
        /// <summary>
        /// 根据条件检索集合中的数据带排序功能
        /// </summary>
        /// <param name="whereLamada"></param>
        /// <returns></returns>
        public List<Person> GetListByCondition<Tkey>(Expression<Func<Person, bool>> whereLamada, Expression<Func<Person, Tkey>> orderLamada)
        {//Tkey 推断 ，使用的时候指定其类型，调用的时候可以忽略
            return ps.GetListByCondition(whereLamada, orderLamada);
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
                return ps.GetPageList(pageIndex, pageSize, orderLamada, whereLamada);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion



    }
}
