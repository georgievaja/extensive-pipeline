using System;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;

namespace Extensive.Pipeline.CacheControl.Pure.Functors
{
    public sealed class Maybe<T> where T : class
    {        
        internal T? Item { get; }

        public static Maybe<T> None => new Maybe<T>();

        [return: NotNull]
        public static Maybe<T> Some(T value)
        {
            return new Maybe<T>(value);
        }

        private Maybe()
        {
            this.Item = null;
        }

        private Maybe(T item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            this.Item = item;
        }

        [return: NotNull]
        public Maybe<TResult> Select<TResult>(
            [DisallowNull] Func<T, TResult> selector)
            where TResult : class
        {
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return this.Item != null ? new Maybe<TResult>(selector(this.Item)) : new Maybe<TResult>();
        }

        [return: NotNull]
        public async Task<Maybe<TResult>> SelectAsync<TResult>(
            [DisallowNull] Func<T, Task<TResult>> selector)
            where TResult : class
        {
            if (selector == null)
                throw new ArgumentNullException(nameof(selector));

            return this.Item != null
                ? new Maybe<TResult>(
                    await selector(this.Item).ConfigureAwait(false))
                : new Maybe<TResult>();
        }

        [return: NotNull]
        public Maybe<TResult> SelectMany<TResult>(
            [DisallowNull] Func<T, Maybe<TResult>> selector)
            where TResult : class
        {
            if (selector == null)
                throw new ArgumentNullException(nameof(selector));

            return this.Item != null ? selector(this.Item) : new Maybe<TResult>();
        }

        [return: NotNull]
        public async Task<Maybe<TResult>> SelectManyAsync<TResult>(
            [DisallowNull] Func<T, Task<Maybe<TResult>>> selector)
            where TResult : class
        {
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return this.Item != null ? await selector(this.Item).ConfigureAwait(false) : new Maybe<TResult>();
        }

        [return: NotNull]
        public TResult MatchResult<TResult>(
            [DisallowNull] TResult nothing,
            [DisallowNull] Func<T, TResult> just)
        {
            if (nothing == null) throw new ArgumentNullException(nameof(nothing));
            if (just == null) throw new ArgumentNullException(nameof(just));

            return this.Item != null ? just(Item) : nothing;
        }

        [return: NotNull]
        public async Task<TResult> MatchResultAsync<TResult>(
            [DisallowNull] TResult nothing,
            [DisallowNull] Func<T, Task<TResult>> just)
        {
            if (nothing == null) throw new ArgumentNullException(nameof(nothing));
            if (just == null) throw new ArgumentNullException(nameof(just));

            return this.Item != null ? await just(Item).ConfigureAwait(false) : nothing;
        }

        [return: NotNull]
        public TResult MatchResult<TResult>(
            [DisallowNull] TResult nothing,
            [DisallowNull] TResult just)
        {
            if (nothing == null) throw new ArgumentNullException(nameof(nothing));
            if (just == null) throw new ArgumentNullException(nameof(just));

            return this.Item != null ? just : nothing;
        }

        [return: NotNull]
        public Func<TResult> Bind<TArg, TResult>(
            [DisallowNull] Func<TArg, TResult> func,
            [DisallowNull] TArg arg)
        {
            if (func == null) throw new ArgumentNullException(nameof(func));
            if (arg == null) throw new ArgumentNullException(nameof(arg));

            return () => func(arg);
        }

        public void Execute(
            [DisallowNull] Action<T> just)
        {
            if (just == null) throw new ArgumentNullException(nameof(just));

            if (Item != null)
            {
                just.Invoke(Item);
            }
        }

        public void Match(
            [DisallowNull] Action<T> just,
            [DisallowNull] Action nothing)
        {
            if (just == null) throw new ArgumentNullException(nameof(just));
            if (nothing == null) throw new ArgumentNullException(nameof(nothing));

            if (this.Item != null)
            {
                just.Invoke(Item);
            }
            else
            {
                nothing.Invoke();
            }
        }

        public async Task MatchAsync(
            [DisallowNull] Func<Task> just,
            [DisallowNull] Action nothing)
        {
            if (just == null) throw new ArgumentNullException(nameof(just));
            if (nothing == null) throw new ArgumentNullException(nameof(nothing));

            if (this.Item != null)
            {
                await (just.Invoke().ConfigureAwait(false));
            }
            else
            {
                nothing.Invoke();
            }
        }

        public async Task MatchAsync(
            [DisallowNull] Func<T, Task> just,
            [DisallowNull] Action nothing)
        {
            if (just == null) throw new ArgumentNullException(nameof(just));
            if (nothing == null) throw new ArgumentNullException(nameof(nothing));

            if (this.Item != null)
            {
                await just.Invoke(Item).ConfigureAwait(false);
            }
            else
            {
                nothing.Invoke();
            }
        }

        [return: NotNull]
        public async Task<TResult> MatchAsync<TResult>(
            [DisallowNull] Func<T, Task<TResult>> just,
            [DisallowNull] Func<Task<TResult>> nothing)
        {
            if (just == null) throw new ArgumentNullException(nameof(just));
            if (nothing == null) throw new ArgumentNullException(nameof(nothing));

            if (this.Item != null)
            {
                return await just.Invoke(Item).ConfigureAwait(false);
            }

            return await nothing.Invoke().ConfigureAwait(false);
        }

        public void Match(
            [DisallowNull] Action just,
            [DisallowNull] Action nothing)
        {
            if (just == null) throw new ArgumentNullException(nameof(just));
            if (nothing == null) throw new ArgumentNullException(nameof(nothing));

            if (this.Item != null)
            {
                just.Invoke();
            }
            else
            {
                nothing.Invoke();
            }
        }

        public override bool Equals([AllowNull] object? obj)
        {
            if (!(obj is Maybe<T> other))
                return false;

            return Equals(this.Item, other.Item);
        }

        public override int GetHashCode()
        {
            return this.Item != null ? this.Item.GetHashCode() : 0;
        }
    }
}