using AspNetMVC5WithNg.Repository.BaseRepository;
using AspNetMVC5WithNg.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspNetMVC5WithNg.Service
{
    public class CustomerService
    {
        private GenericRepository<Customer> CustRepository;

        public CustomerService()
        { 
            this.CustRepository = new GenericRepository<Customer>(new CRUD_SampleEntities());
        }

        public IEnumerable<Customer> GetAll(object[] parameters)
        {
                            //[存储过程名]空格{参数0}
            string spQuery = "[Get_Customer] {0}";
            return CustRepository.ExecuteQuery(spQuery, parameters);
        }

        public Customer GetbyID(object[] parameters)
        {
            string spQuery = "[Get_CustomerbyID] {0}";
            return CustRepository.ExecuteQuerySingle(spQuery, parameters);
        }

        public int Insert(object[] parameters)
        {   
                          //[存储过程名]空格{参数0},{参数1}
            string spQuery = "[Set_Customer] {0}, {1}";
            return CustRepository.ExecuteCommand(spQuery, parameters);
        }

        public int Update(object[] parameters)
        {
            string spQuery = "[Update_Customer] {0}, {1}, {2}";
            return CustRepository.ExecuteCommand(spQuery, parameters);
        }

        public int Delete(object[] parameters)
        {
            string spQuery = "[Delete_Customer] {0}";
            return CustRepository.ExecuteCommand(spQuery, parameters);
        }
    }
}