using Orbita.Entity;

namespace Orbita.Interface
{
    public interface IRepository<T> where T : Entitys
    {
        /// <summary>
        /// Retorna uma lista da entidade selecionada
        /// </summary>
        /// <returns></returns>
        IList<T> GetAll();
       
        
        /// <summary>
        /// Obtem a entidade selecionada pelo Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T GetById(int id);
       
        
        /// <summary>
        /// Cria uma instancia da entidade selecionada
        /// </summary>
        /// <param name="entity"></param>
        void Save(T  entity);


        /// <summary>
        /// Altera uma propriedade da instância da entidade selecionada
        /// </summary>
        /// <param name="entity"></param>        
        void Put(T entity);



        /// <summary>
        /// Deleta a instancia da entidade selecionada pelo Id
        /// </summary>
        /// <param name="id"></param>
        void Delete(int id);
    }
}
