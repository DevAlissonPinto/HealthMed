using HealthMed.Domain.Entities.Base;
using HealthMed.Domain.Extensions;
using HealthMed.Domain.Helpers;
using HealthMed.Domain.Interfaces;
using HealthMed.Domain.Interfaces.Repositories;
using HealthMed.Infra.Repository.Extensions;
using FastMember;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.Common;
using System.Linq.Expressions;
using System.Reflection;

namespace HealthMed.Infra.Repository.Repositories;

public class BaseRepository<TContext, TEntity> : IBaseRepository<TContext, TEntity>
     where TEntity : EntityBase
     where TContext : IUnitOfWork<TContext>
{
    protected IUnitOfWork<TContext> UnitOfWork { get; }
    public readonly DbContext _context;
    public SqlConnection _connection;
    public readonly string _connectionString = string.Empty;
    public readonly string _startSufix = "start";
    public readonly string _endSufix = "end";
    public IDbTransaction _transaction;

    public BaseRepository(IUnitOfWork<TContext> unitOfWork)
    {
        this.UnitOfWork = unitOfWork;
        _context = ((DbContext)unitOfWork);
        _connectionString = _context.Database.GetConnectionString();
    }
    protected DbSet<TEntity> DbSet => _context.Set<TEntity>();
    public virtual TEntity Save(TEntity entity)
    {
        DbSet.Add(entity);
        return entity;
    }
    public async Task<TEntity> SaveAsync(TEntity entity)
    {
        await _context.Set<TEntity>().AddAsync(entity);
        return entity;
    }
    public virtual void SaveRange(IEnumerable<TEntity> entities)
    {
        DbSet.AddRange(entities);
    }
    public async Task SaveRangeAsync(IEnumerable<TEntity> entities)
    {
        await _context.Set<TEntity>().AddRangeAsync(entities);
    }
    public virtual TEntity Update(TEntity entity)
    {
        var result = DbSet.Attach(entity);
        result.State = EntityState.Modified;
        var entry = ((DbContext)UnitOfWork).Entry(entity);

        if (entry.Metadata.FindProperty("DataInclusao") != null)
            entry.Property(x => x.DataInclusao).IsModified = false;
        if (entry.Metadata.FindProperty("UsuarioCadastro") != null)
            entry.Property(x => x.UsuarioInclusao).IsModified = false;
        return entity;
    }
    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        var result = _context.Set<TEntity>().Attach(entity);
        result.State = EntityState.Modified;
        var entry = _context.Entry(entity);

        if (entry.Metadata.FindProperty("DataInclusao") != null)
            entry.Property("DataInclusao").IsModified = false;
        if (entry.Metadata.FindProperty("UsuarioCadastro") != null)
            entry.Property("UsuarioInclusao").IsModified = false;

        return entity;
    }
    public virtual void UpdateRange(IEnumerable<TEntity> entities)
    {
        DbSet.AttachRange(entities);
    }
    public async Task UpdateRangeAsync(IEnumerable<TEntity> entities)
    {
        _context.Set<TEntity>().AttachRange(entities);
        _context.UpdateRange(entities);
    }
    public virtual void Delete(int codigo)
    {
        TEntity entity = Get(codigo);
        entity.AtualizarDataAlteracao();
        entity.Inativar();
        DbSet.Update(entity);
    }
    public async Task DeleteAsync(int codigo)
    {
        TEntity entity = await GetAsync(codigo);
        if (entity != null)
        {
            entity.AtualizarDataAlteracao();
            entity.Inativar();
            _context.Set<TEntity>().Update(entity);
        }
    }
    public virtual void Delete(TEntity entity)
    {
        DbSet.Update(entity);
    }
    public async Task DeleteAsync(TEntity entity)
    {
        _context.Set<TEntity>().Update(entity);
    }
    public virtual TEntity Get(int id)
    {
        return DbSet.FirstOrDefault(p => p.Id == id);
    }
    public virtual TEntity Get(int id, params Expression<Func<TEntity, object>>[] includeProperties)
    {
        IQueryable<TEntity> query = DbSet;
        foreach (var includeProperty in includeProperties)
        {
            query = query.Include(includeProperty);
        }
        return query.FirstOrDefault(p => p.Id == id);
    }
    public virtual TEntity GetNoTracking(int id)
    {
        return DbSet.AsNoTracking()
            .FirstOrDefault(p => p.Id == id);
    }
    public virtual async Task<TEntity> GetAsync(int id)
    {
        return await DbSet.FirstOrDefaultAsync(p => p.Id == id);
    }
    public virtual async Task<TEntity> GetAsync(int id, params Expression<Func<TEntity, object>>[] includeProperties)
    {
        IQueryable<TEntity> query = DbSet;
        foreach (var includeProperty in includeProperties)
        {
            query = query.Include(includeProperty);
        }
        return await query.FirstOrDefaultAsync(p => p.Id == id);
    }
    public virtual async Task<TEntity> GetNoTrackingAsync(int id)
    {
        return await DbSet.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
    }
    public virtual async Task<TEntity> GetNoTrackingAsync(int id, params Expression<Func<TEntity, object>>[] includeProperties)
    {
        IQueryable<TEntity> query = DbSet;
        foreach (var includeProperty in includeProperties)
        {
            query = query.Include(includeProperty);
        }
        return await query.FirstOrDefaultAsync(p => p.Id == id);
    }
    public virtual IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null)
    {
        if (predicate == null)
            return DbSet
                .Where(T => T.Ativo)
                .AsNoTracking()
                .ToList();
        else
            return DbSet
                .Where(predicate)
                .Where(T => T.Ativo)
                .AsNoTracking()
                .ToList();
    }

    //get all, including where clause, order by clause, and includes
    //usage: var s = repository.GetAll(i => i => i.Name.Contains('John'), i => i.NavigationProperty)
    public virtual IEnumerable<TEntity> GetAll(Expression<Func<TEntity, Boolean>> whereExp,
                                                  params Expression<Func<TEntity, object>>[] includeExps)
    {
        var query = DbSet.AsQueryable();

        if (whereExp != null)
            query = query.Where(whereExp);

        if (includeExps != null)
            query = includeExps.Aggregate(query, (current, exp) => current.Include(exp));

        return query.ToList();
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null)
    {
        if (predicate == null)
            return await DbSet.Where(T => T.Ativo).AsNoTracking().ToListAsync();
        else
            return await DbSet.Where(predicate).Where(T => T.Ativo).AsNoTracking().ToListAsync();
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate,
                                                      params Expression<Func<TEntity, object>>[] includeExps)
    {
        var query = DbSet.AsQueryable();

        if (predicate != null)
            query = query.Where(predicate);

        if (includeExps != null)
            query = includeExps.Aggregate(query, (current, exp) => current.Include(exp));

        return await query.ToListAsync();
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllNoTrackingAsync(Expression<Func<TEntity, bool>> predicate,
                                                     params Expression<Func<TEntity, object>>[] includeExps)
    {
        var query = DbSet.AsNoTracking().AsQueryable();

        if (predicate != null)
            query = query.Where(predicate);

        if (includeExps != null)
            query = includeExps.Aggregate(query, (current, exp) => current.Include(exp));

        return await query.ToListAsync();
    }

    public IEnumerable<TEntity> GetFilteredAndInclude(
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? limit = null,
            params Expression<Func<TEntity, object>>[] includeProperties)
    {
        // Inicie a consulta com o DbSet
        IQueryable<TEntity> query = DbSet;

        // Aplique o predicado se fornecido
        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        // Aplique as propriedades de inclusão se fornecidas
        if (includeProperties != null)
        {
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
        }

        // Aplique a ordenação se fornecida
        if (orderBy != null)
        {
            query = orderBy(query);
        }

        // Aplique o limite se fornecido
        if (limit.HasValue)
        {
            query = query.Take(limit.Value);
        }

        // Execute a consulta e retorne os resultados
        return query.ToList();
    }

    public virtual IQueryable<TEntity> GetQueryable(Expression<Func<TEntity, bool>> predicate = null)
    {
        if (predicate == null)
            return DbSet.AsQueryable();
        else
            return DbSet.Where(predicate).AsQueryable();
    }

    public virtual IPagedList<TEntity> GetPaginated(QueryFilter filter, int start = 0, int limit = 10, bool orderByDescending = true, params Expression<Func<TEntity, object>>[] includes)
    {
        var result = DbSet.AsQueryable().Where(x => x.Ativo);

        if (includes != null)
            foreach (var include in includes)
                result = result.Include(include);


        return GetPagedList(result, filter, start, limit, orderByDescending);
    }
    public virtual async Task<IPagedList<TEntity>> GetPaginatedAsync(QueryFilter filter, int start = 0, int limit = 10, bool orderByDescending = true, params Expression<Func<TEntity, object>>[] includes)
    {
        var result = DbSet.AsQueryable().Where(x => x.Ativo);

        if (includes != null)
            foreach (var include in includes)
                result = result.Include(include);

        return await GetPagedListAsync(result, filter, start, limit, orderByDescending);
    }
    public virtual async Task<IPagedList<TEntity>> GetPaginatedAsync(QueryFilter filter, int start = 0, int limit = 10, string orderByProperty = "Id", bool orderByDescending = true, params Expression<Func<TEntity, object>>[] includes)
    {
        var result = DbSet.AsQueryable().Where(x => x.Ativo);

        if (includes != null)
        {
            foreach (var include in includes)
            {
                result = result.Include(include);
            }
        }

        result = result.OrderBy(orderByProperty, orderByDescending);

        if (filter != null)
        {
            result = AddQueryFilter(filter, result);
        }

        var total = await result.CountAsync();

        if (limit > 0)
        {
            result = result
                .Skip(start)
                .Take(limit);
        }

        var list = await result.ToListAsync();

        return list.ToPagedList(total);
    }

    protected IPagedList<TEntity> GetPagedList(IQueryable<TEntity> result, QueryFilter filter, int start, int limit, bool orderByDescending)
    {
        if (filter != null)
            result = AddQueryFilter(filter, result);

        if (orderByDescending)
            result = result.OrderByDescending(x => x.Id);

        var total = result.Count();

        if (limit > 0)
        {
            result = result
                .Skip(start)
                .Take(limit);
        }

        return result
            .ToPagedList(total);
    }
    protected async Task<IPagedList<TEntity>> GetPagedListAsync(IQueryable<TEntity> result, QueryFilter filter, int start, int limit, bool orderByDescending)
    {
        if (filter != null)
            result = AddQueryFilter(filter, result);

        if (orderByDescending)
            result = result.OrderByDescending(x => x.Id);

        var total = await result.CountAsync();

        if (limit > 0)
        {
            result = result
                .Skip(start)
                .Take(limit);
        }

        var list = await result.ToListAsync();
        return list.ToPagedList(total);
    }


    #region Bulk Insert
    public virtual async Task BulkInsertAsync(ICollection<TEntity> dados, string[] colunas, string table)
    {
        using (var sqlCopy = new SqlBulkCopy(_connectionString, SqlBulkCopyOptions.Default))
        {
            sqlCopy.DestinationTableName = table;

            using (var reader = ObjectReader.Create(dados, colunas))
                await sqlCopy.WriteToServerAsync(reader);
        }
    }

    public virtual void BulkInsert(ICollection<TEntity> dados, string[] colunas, string table)
    {
        using (var sqlCopy = new SqlBulkCopy(_connectionString, SqlBulkCopyOptions.Default))
        {
            sqlCopy.DestinationTableName = table;

            using (var reader = ObjectReader.Create(dados, colunas))
                sqlCopy.WriteToServer(reader);
        }
    }
    public virtual void BulkInsert<T>(ICollection<T> dados, string[] colunas, string table)
    {
        using (var sqlCopy = new SqlBulkCopy(_connectionString, SqlBulkCopyOptions.Default))
        {
            sqlCopy.DestinationTableName = table;

            using (var reader = ObjectReader.Create(dados, colunas))
                sqlCopy.WriteToServer(reader);
        }
    }

    #endregion

    #region Metodos QueryFilter

    private static readonly string[] listaNumeros = { "INT16", "INT32", "INT64", "DECIMAL", "DOUBLE", "BYTE", "SBYTE", "FLOAT", "UINT16", "UINT32", "UINT64" };

    protected IQueryable<TEntity> AddQueryFilter(QueryFilter filter, IQueryable<TEntity> pesquisa)
    {
        var tipo = typeof(TEntity);
        Expression<Func<TEntity, bool>> exp = null;
        Expression<Func<TEntity, bool>> expAnd = null;

        if (filter.Filters != null)
            foreach (QueryFilter.FilterContainer filterContainer in filter.Filters)
            {
                PropertyInfo property = tipo.GetProperty(filterContainer.Property, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                if (filterContainer.Property.Contains(_startSufix, StringComparison.OrdinalIgnoreCase))
                {
                    string propriedadeData = filterContainer.Property.Replace(_startSufix, "", StringComparison.OrdinalIgnoreCase);
                    property = tipo.GetProperty(propriedadeData, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                }
                if (filterContainer.Property.Contains(_endSufix, StringComparison.OrdinalIgnoreCase))
                {
                    string propriedadeData = filterContainer.Property.Replace(_endSufix, "", StringComparison.OrdinalIgnoreCase);
                    property = tipo.GetProperty(propriedadeData, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                }

                if (property == null)
                    continue;

                Type propertyType = property.PropertyType;
                var isNullable = Nullable.GetUnderlyingType(property.PropertyType) != null;
                object valor = null;

                if (isNullable)
                    valor = ObterParametroValor(filterContainer, propertyType, true);
                else
                {
                    valor = ObterParametroValor(filterContainer, propertyType);
                    if (valor == null)
                        continue;
                }


                var param = Expression.Parameter(typeof(TEntity), "p");

                if (filterContainer.TypeFilterAnd)
                {
                    if (propertyType.Name.ToLower() == "string")
                    {
                        expAnd = expAnd == null
                            ? GetExpression<TEntity>(filterContainer.Property, filterContainer.Value.ToString().ToUpper())
                            : GetExpression<TEntity>(filterContainer.Property, filterContainer.Value.ToString().ToUpper()).And(expAnd);
                    }
                    else if (valor?.GetType().IsGenericType ?? false)
                    {
                        var propertyExp = Expression.Property(param, property);
                        var method = valor.GetType().GetMethod("Contains");
                        var call = Expression.Call(Expression.Constant(valor), method, propertyExp);
                        var lambda = Expression.Lambda<Func<TEntity, bool>>(call, param);
                        expAnd = (expAnd == null) ? lambda : lambda.And(expAnd);
                    }
                    else if (propertyType.Name.ToLower() == "datetime")
                    {
                        BinaryExpression expressao = Expression.Equal(Expression.Property(param, property), Expression.Constant(valor, propertyType));
                        if (filterContainer.Property.Contains(_startSufix, StringComparison.OrdinalIgnoreCase))
                        {
                            expressao = Expression.GreaterThanOrEqual(Expression.Property(param, property), Expression.Constant(valor, propertyType));
                        }
                        else if (filterContainer.Property.Contains(_endSufix, StringComparison.OrdinalIgnoreCase))
                        {
                            expressao = Expression.LessThanOrEqual(Expression.Property(param, property), Expression.Constant(valor, propertyType));
                        }

                        expAnd = expAnd == null
                                ? Expression.Lambda<Func<TEntity, bool>>(expressao, param)
                                : Expression.Lambda<Func<TEntity, bool>>(expressao, param).And(expAnd);
                    }
                    else
                    {
                        var expressao = Expression.Equal(Expression.Property(param, property), Expression.Constant(valor, propertyType));
                        expAnd = expAnd == null
                                ? Expression.Lambda<Func<TEntity, bool>>(expressao, param)
                                : Expression.Lambda<Func<TEntity, bool>>(expressao, param).And(expAnd);
                    }
                }
                else
                {
                    if (propertyType.Name == "string" || propertyType.Name == "String")
                    {
                        exp = exp == null
                            ? GetExpression<TEntity>(filterContainer.Property, filterContainer.Value.ToString().ToUpper())
                            : GetExpression<TEntity>(filterContainer.Property, filterContainer.Value.ToString().ToUpper()).Or(exp);
                    }
                    else
                    {
                        var expressao = Expression.Equal(Expression.Property(param, property), Expression.Constant(valor, propertyType));
                        exp = exp == null
                            ? Expression.Lambda<Func<TEntity, bool>>(expressao, param)
                            : Expression.Lambda<Func<TEntity, bool>>(expressao, param).Or(exp);
                    }
                }
            }

        if (exp != null)
            pesquisa = pesquisa.Where(exp);

        if (expAnd != null)
            pesquisa = pesquisa.Where(expAnd);

        return pesquisa;
    }

    private object ObterParametroValor(QueryFilter.FilterContainer filterContainer, Type propertyType, bool nulable = false)
    {
        object result = null;
        var propTypeWithoutNull = propertyType;
        if (nulable)
            propTypeWithoutNull = Nullable.GetUnderlyingType(propertyType);
        if (filterContainer.Value == null)
            result = null;
        else if (propertyType.GetTypeInfo().IsEnum)
            result = Enum.Parse(propertyType.GetGenericArguments()[0], filterContainer.Value.ToString(), true);
        else if (listaNumeros.Contains(propTypeWithoutNull.Name.ToUpper()))
        {
            long numero;
            if (!Int64.TryParse(filterContainer.Value.ToString(), out numero))
            {
                string[] ids = filterContainer.Value.ToString().Split(",");
                if (ids.Count() == 1 && Int64.TryParse(ids[0], out numero))
                {
                    result = Convert.ChangeType(ids[0], propertyType);
                }
                else if (ids.Count() > 1 && nulable)
                {
                    var listIds = new List<int?>();
                    foreach (var id in ids)
                        listIds.Add(Int32.Parse(id));
                    result = listIds;
                }
                else if (ids.Count() > 1 && !nulable)
                {
                    var listIds = new List<int>();
                    foreach (var id in ids)
                        listIds.Add(Int32.Parse(id));
                    result = listIds;
                }
            }
            else
                result = Convert.ChangeType(filterContainer.Value.ToString(), propTypeWithoutNull);
        }
        else
            result = Convert.ChangeType(filterContainer.Value.ToString(), propTypeWithoutNull);
        return result;
    }

    private static Expression<Func<T, bool>> GetExpression<T>(string propertyName, string propertyValue)
    {
        var parameterExp = Expression.Parameter(typeof(T), "type");
        var propertyExp = Expression.Property(parameterExp, propertyName);
        MethodInfo method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
        var someValue = Expression.Constant(propertyValue, typeof(string));
        var containsMethodExp = Expression.Call(propertyExp, method, someValue);

        return Expression.Lambda<Func<T, bool>>(containsMethodExp, parameterExp);

    }

    #endregion

    #region "StoredProcedure"
    public List<T> ExecuteStoredProcedure<T, F>(F filters, string procedureName)
    {
        procedureName = procedureName ?? $"Get{typeof(T).Name}";

        return ExecuteReader(procedureName, GetParameter(filters, procedureName)).ToList<T>();
    }
    public List<T> ExecuteStoredProcedureWithParam<T>(string procedureName, IDataParameter[] parameters)
    {
        procedureName = procedureName ?? $"Get{typeof(T).Name}";

        return ExecuteReader(procedureName, parameters).ToList<T>();
    }
    public List<T> ExecuteStoredProcedureWithParam<T>(string procedureName, IDataParameter[] parameters, dynamic transaction, int? commandTimeOut = null)
    {
        procedureName = procedureName ?? $"Get{typeof(T).Name}";

        using IDataReader er = ExecuteReader(procedureName, parameters, transaction, commandTimeOut);
        return er.ToList<T>();
    }
    public int ExecuteStoredProcedureWithParamNonQuery<T>(string procedureName, IDataParameter[] parameters)
    {
        procedureName = procedureName ?? $"Get{typeof(T).Name}";

        return ExecuteNonQuery(procedureName, parameters, CommandType.StoredProcedure);
    }
    public int ExecuteStoredProcedureWithParamNonQuery<T>(string procedureName, IDataParameter[] parameters, dynamic transaction = null, int? commandTimeout = null)
    {
        procedureName = procedureName ?? $"Get{typeof(T).Name}";

        return ExecuteNonQuery(procedureName, parameters, CommandType.StoredProcedure, transaction, commandTimeout);
    }
    public int ExecuteStoredProcedureWithParamScalar<T>(string procedureName, IDataParameter[] parameters)
    {
        procedureName = procedureName ?? $"Get{typeof(T).Name}";

        return ExecuteScalar(procedureName, parameters, CommandType.StoredProcedure).ToInt32();
    }

    private DbConnection GetConnection()
    {
        if (_connection == null) _connection = new SqlConnection(_connectionString);//TODO Resolver ConnectionString
        using (var command = _connection.CreateCommand())
        {
            var connection = _context.Database.GetDbConnection();

            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            return connection;
        }
    }

    public IDataReader ExecuteReader(string commandText, IDataParameter[] parameters = null, SqlTransaction transaction = null, int? commandTimeOut = null)
    {
        using (var command = GetConnection().CreateCommand())
        {
            command.CommandText = commandText;
            command.CommandType = CommandType.StoredProcedure;

            if (transaction != null)
                command.Transaction = transaction;

            command.CommandTimeout = commandTimeOut ?? command.CommandTimeout;

            if (parameters != null)
                foreach (var parameter in parameters)
                    command.Parameters.Add(parameter);

            return command.ExecuteReader(CommandBehavior.CloseConnection);
        }
    }

    public SqlParameter[] GetParameter(dynamic input, string procedureName)
    {
        SqlParameter[] parameters = SqlHelperParameterCache.GetSpParameterSet(_connectionString, procedureName);
        Type inputType = input.GetType();

        foreach (SqlParameter parameter in parameters)
        {
            PropertyInfo property = inputType.GetProperty(parameter.ParameterName.Replace("@", ""));

            if (property == null)
                property = inputType.GetProperty(parameter.ParameterName.Replace("@", "").ToUpper());

            if (property == null)
                property = inputType.GetProperty(parameter.ParameterName.Replace("@", "").ToLower());

            if (property != null)
                parameter.Value = property.GetValue(input, null);
        }

        return parameters.Where(p => p.Value != null).ToArray();
    }

    public int ExecuteNonQuery(string commandText, IDataParameter[] parameters, CommandType commandType, dynamic transaction = null, int? commandTimeOut = null)
    {
        if (_connection == null) _connection = new SqlConnection(_connectionString);
        if (_connection.State != ConnectionState.Open) _connection.Open();

        using (var command = _connection.CreateCommand())
        {
            command.CommandText = commandText;
            command.CommandType = commandType;

            if (_transaction != null)
                command.Transaction = (SqlTransaction)_transaction;

            command.CommandTimeout = commandTimeOut ?? command.CommandTimeout;

            if (parameters != null)
                foreach (var parameter in parameters)
                    command.Parameters.Add(parameter);

            return command.ExecuteNonQuery();
        }
    }

    public object ExecuteProcedureScalar(string procedureName, dynamic parameter)
    {
        if (_connection == null) _connection = new SqlConnection(_connectionString);

        IDataParameter[] parameters = GetParameter(parameter, procedureName);
        using (var command = _connection.CreateCommand())
        {
            command.CommandText = procedureName;
            command.CommandType = CommandType.StoredProcedure;

            if (parameters != null)
                command.Parameters.AddRange(parameters);

            if (_connection.State != ConnectionState.Open) _connection.Open();
            return command.ExecuteScalar();
        }
    }

    public object ExecuteScalar(string commandText, IDataParameter[] parameters, CommandType commandType)
    {
        if (_connection == null) _connection = new SqlConnection(_connectionString);
        if (_connection.State != ConnectionState.Open) _connection.Open();

        using (var command = _connection.CreateCommand())
        {
            command.CommandText = commandText;
            command.CommandType = commandType;

            if (parameters != null)
                foreach (var parameter in parameters)
                    command.Parameters.Add(parameter);

            return command.ExecuteScalar();
        }
    }

    #endregion

    #region "Transactions"
    public virtual void ClearTransaction()
    {
        var entries = _context.ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Added ||
                        e.State == EntityState.Modified ||
                        e.State == EntityState.Deleted);

        foreach (var entry in entries)
        {
            entry.State = EntityState.Detached;
        }
    }
    public virtual void BeginTransaction()
    {
        if (_connection == null) _connection = new SqlConnection(_connectionString);
        if (_connection.State != ConnectionState.Open) _connection.Open();
        _transaction = _connection.BeginTransaction();
    }

    public virtual void CommitTransaction()
    {
        _transaction.Commit();
        _transaction.Dispose();

        if (_connection.State == System.Data.ConnectionState.Open) _connection.Close();
        _connection.Dispose();

        _connection = null;
        _transaction = null;
    }

    public virtual void RollbackTransaction()
    {
        _transaction.Rollback();
        _transaction.Dispose();

        if (_connection.State == System.Data.ConnectionState.Open) _connection.Close();
        _connection.Dispose();

        _connection = null;
        _transaction = null;
    }



    #endregion

}
