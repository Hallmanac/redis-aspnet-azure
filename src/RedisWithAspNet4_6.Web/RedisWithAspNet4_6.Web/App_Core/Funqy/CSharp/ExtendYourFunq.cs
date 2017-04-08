using System;
using System.Threading.Tasks;


namespace Funqy.CSharp
{
    public static class ExtendYourFunq
    {
        #region Get Funqy

        public static FunqResult<T> GetFunqy<T>(this T @this)
        {
            return @this == null ? FunqFactory.Fail<T>("Nothing to get Funqy with", null) : FunqFactory.KeepGroovin(@this);
        }


        public static FunqResult<Guid> GetFunqy(this Guid @this)
        {
            return @this == default(Guid)
                       ? FunqFactory.Fail<Guid>("The Guid provided to get funqy was the default Guid value and invalid", null)
                       : FunqFactory.KeepGroovin(@this);
        }


        public static async Task<FunqResult<T>> GetFunqyAsync<T>(this T @this)
        {
            return await Task.FromResult(@this == null ? FunqFactory.Fail<T>("Nothing to get Funqy with", null) : FunqFactory.KeepGroovin(@this))
                             .ConfigureAwait(false);
        }


        public static async Task<FunqResult<T>> GetFunqyAsync<T>(this Task<T> thisTask)
        {
            var @this = await thisTask;
            return await Task.FromResult(@this == null ? FunqFactory.Fail<T>("Nothing to get Funqy with", null) : FunqFactory.KeepGroovin(@this))
                             .ConfigureAwait(false);
        }


        public static async Task<FunqResult<Guid>> GetFunqyAsync(this Guid @this)
        {
            return await Task.FromResult(@this == default(Guid)
                                             ? FunqFactory.Fail<Guid>("The Guid provided to get funqy was the default Guid value and invalid", null)
                                             : FunqFactory.KeepGroovin(@this))
                             .ConfigureAwait(false);
        }

        #endregion


        #region Then

        public static FunqResult<TOutput> Then<TOutput>(this FunqResult @this, Func<FunqResult<TOutput>> callback)
        {
            if (@this.IsFailure)
            {
                return FunqFactory.Fail<TOutput>(@this.Message, null);
            }
            var funq = callback();
            return funq;
        }


        public static FunqResult<TOutput> Then<TInput, TOutput>(this FunqResult<TInput> @this, Func<TInput, FunqResult<TOutput>> callback)
        {
            if (@this.IsFailure)
            {
                return FunqFactory.Fail(@this.Message, default(TOutput));
            }
            var result = callback(@this.Result);
            return result;
        }


        public static FunqResult Then(this FunqResult @this, Func<FunqResult> callback)
        {
            return @this.IsFailure ? FunqFactory.Fail(@this.Message) : callback();
        }


        public static FunqResult Then<TInput>(this FunqResult<TInput> @this, Func<TInput, FunqResult> callback)
        {
            return @this.IsFailure ? FunqFactory.Fail(@this.Message) : callback(@this.Result);
        }
        #endregion
        
        #region Then Async

        public static async Task<FunqResult<TOutput>> ThenAsync<TOutput>(this Task<FunqResult> thisTask, Func<Task<FunqResult<TOutput>>> callback)
        {
            var @this = await thisTask;
            if (@this.IsFailure)
            {
                return await Task.FromResult(FunqFactory.Fail<TOutput>(@this.Message, null)).ConfigureAwait(false);
            }
            var funq = await callback().ConfigureAwait(false);
            return funq;
        }


        public static async Task<FunqResult<TOutput>> ThenAsync<TOutput>(this Task<FunqResult<TOutput>> thisTask, Func<TOutput, Task<FunqResult<TOutput>>> callback)
        {
            var @this = await thisTask;
            if (@this.IsFailure)
            {
                return await Task.FromResult(FunqFactory.Fail(@this.Message, default(TOutput)));
            }
            var funq = await callback(@this.Result).ConfigureAwait(false);
            return funq;
        }


        public static async Task<FunqResult<TOutput>> ThenAsync<TInput, TOutput>(this Task<FunqResult<TInput>> thisTask, Func<TInput, Task<FunqResult<TOutput>>> callback)
        {
            var @this = await thisTask.ConfigureAwait(false);
            if (@this.IsFailure)
            {
                return await Task.FromResult(FunqFactory.Fail(@this.Message, default(TOutput))).ConfigureAwait(false);
            }
            var funq = await callback(@this.Result).ConfigureAwait(false);
            return funq;
        }


        public static async Task<FunqResult> ThenAsync(this Task<FunqResult> thisTask, Func<Task<FunqResult>> callback)
        {
            var @this = await thisTask;
            if (@this.IsFailure)
            {
                return await Task.FromResult(FunqFactory.Fail(@this.Message)).ConfigureAwait(false);
            }
            var funq = await callback().ConfigureAwait(false);
            return funq;
        }


        public static async Task<FunqResult> ThenAsync<TInput>(this Task<FunqResult<TInput>> thisTask, Func<TInput, Task<FunqResult>> callback)
        {
            var @this = await thisTask;
            if (@this.IsFailure)
            {
                return await Task.FromResult(FunqFactory.Fail(@this.Message)).ConfigureAwait(false);
            }
            var funq = await callback(@this.Result).ConfigureAwait(false);
            return funq;
        }

        #endregion


        #region Catch 

        public static FunqResult<TOutput> Catch<TOutput>(this FunqResult<TOutput> @this, Func<FunqResult<TOutput>> callback)
        {
            if (@this.IsSuccessful)
            {
                return FunqFactory.KeepGroovin(@this.Result, @this.Message);
            }
            var errorFunq = callback();
            return errorFunq;
        }


        public static FunqResult<TOutput> Catch<TInput, TOutput>(this FunqResult<TInput> @this, Func<FunqResult<TInput>, FunqResult<TOutput>> callback)
        {
            // Catch should always call its callback. The Implementation of the catch should be responsible for returning an OK or a Fail
            var errorFunq = callback(@this);
            return errorFunq;
        }


        public static FunqResult Catch<TInput>(this FunqResult<TInput> @this, Func<FunqResult> callback)
        {
            if (@this.IsSuccessful)
            {
                return FunqFactory.KeepGroovin(@this.Message);
            }
            var funq = callback();
            return funq;
        }


        public static FunqResult Catch(this FunqResult @this, Func<FunqResult> callback)
        {
            if (@this.IsSuccessful)
            {
                return FunqFactory.KeepGroovin(@this.Message);
            }
            var errorFunq = callback();
            return errorFunq;
        }

        #endregion

        #region Catch Async

        public static async Task<FunqResult<TOutput>> CatchAsync<TOutput>(this Task<FunqResult<TOutput>> thisTask, Func<Task<FunqResult<TOutput>>> callback)
        {
            var @this = await thisTask.ConfigureAwait(false);
            if (@this.IsSuccessful)
            {
                return await Task.FromResult(FunqFactory.KeepGroovin(@this.Result, @this.Message)).ConfigureAwait(false);
            }
            var errorFunq = await callback().ConfigureAwait(false);
            return errorFunq;
        }


        public static async Task<FunqResult<TOutput>> CatchAsync<TInput, TOutput>(this Task<FunqResult<TInput>> thisTask, Func<FunqResult<TInput>, Task<FunqResult<TOutput>>> callback)
        {
            var @this = await thisTask.ConfigureAwait(false);
            
            var errorFunq = await callback(@this).ConfigureAwait(false);
            return errorFunq;
        }


        public static async Task<FunqResult> CatchAsync<TInput>(this Task<FunqResult<TInput>> thisTask, Func<Task<FunqResult>> callback)
        {
            var @this = await thisTask.ConfigureAwait(false);
            if (@this.IsSuccessful)
            {
                return await Task.FromResult(FunqFactory.KeepGroovin(@this.Message)).ConfigureAwait(false);
            }
            var funq = await callback().ConfigureAwait(false);
            return funq;
        }


        public static async Task<FunqResult> CatchAsync(this Task<FunqResult> thisTask, Func<Task<FunqResult>> callback)
        {
            var @this = await thisTask.ConfigureAwait(false);
            if (@this.IsSuccessful)
            {
                return await Task.FromResult(FunqFactory.KeepGroovin(@this.Message)).ConfigureAwait(false);
            }
            var errorFunq = await callback().ConfigureAwait(false);
            return errorFunq;
        }

        #endregion


        #region Finally

        public static FunqResult<TOutput> Finally<TInput, TOutput>(this FunqResult<TInput> @this, Func<FunqResult<TInput>, FunqResult<TOutput>> callback)
        {
            var result = @this.IsSuccessful ? callback(FunqFactory.KeepGroovin(@this, @this.Message)) : callback(FunqFactory.Fail(@this.Message, @this));
            return result;
        }


        public static FunqResult Finally<TInput>(this FunqResult<TInput> @this, Func<FunqResult<TInput>, FunqResult> callback)
        {
            var result = @this.IsSuccessful 
                ? callback(FunqFactory.KeepGroovin(@this, @this.Message)) 
                : callback(FunqFactory.Fail(@this.Message, @this));
            return result;
        }


        public static FunqResult<TOutput> Finally<TOutput>(this FunqResult @this, Func<FunqResult, FunqResult<TOutput>> callback)
        {
            var result = @this.IsSuccessful 
                ? callback(FunqFactory.KeepGroovin(@this.Message)) 
                : callback(FunqFactory.Fail(@this.Message));
            return result;
        }


        public static FunqResult Finally(this FunqResult @this, Func<FunqResult, FunqResult> callback)
        {
            var result = @this.IsSuccessful ? callback(FunqFactory.KeepGroovin(@this)) : callback(FunqFactory.Fail(@this.Message, @this));
            return result;
        }

        #endregion

        #region Finally Async

        public static async Task<FunqResult<TOutput>> FinallyAsync<TInput, TOutput>(this Task<FunqResult<TInput>> thisAsync, Func<FunqResult<TInput>, Task<FunqResult<TOutput>>> callback)
        {
            var @this = await thisAsync.ConfigureAwait(false);
            var result = @this.IsSuccessful
                             ? await callback(FunqFactory.KeepGroovin(@this, @this.Message)).ConfigureAwait(false)
                             : await callback(FunqFactory.Fail(@this.Message, @this)).ConfigureAwait(false);
            return result;
        }


        public static async Task<FunqResult<TOutput>> FinallyAsync<TOutput>(this Task<FunqResult> thisAsync, Func<FunqResult, Task<FunqResult<TOutput>>> callback)
        {
            var @this = await thisAsync.ConfigureAwait(false);
            var result = @this.IsSuccessful
                             ? await callback(FunqFactory.KeepGroovin(@this, @this.Message)).ConfigureAwait(false)
                             : await callback(FunqFactory.Fail(@this.Message, @this)).ConfigureAwait(false);
            return result;
        }


        public static async Task<FunqResult> FinallyAsync<TInput>(this Task<FunqResult<TInput>> thisAsync, Func<FunqResult<TInput>, Task<FunqResult>> callback)
        {
            var @this = await thisAsync.ConfigureAwait(false);
            var result = @this.IsSuccessful
                             ? await callback(FunqFactory.KeepGroovin(@this, @this.Message)).ConfigureAwait(false)
                             : await callback(FunqFactory.Fail(@this.Message, @this)).ConfigureAwait(false);
            return result;
        }


        public static async Task<FunqResult> FinallyAsync(this Task<FunqResult> thisAsync, Func<FunqResult, Task<FunqResult>> callback)
        {
            var @this = await thisAsync.ConfigureAwait(false);
            var result = @this.IsSuccessful
                             ? await callback(FunqFactory.KeepGroovin(@this)).ConfigureAwait(false)
                             : await callback(FunqFactory.Fail(@this.Message, @this)).ConfigureAwait(false);
            return result;
        }

        #endregion
    }
}