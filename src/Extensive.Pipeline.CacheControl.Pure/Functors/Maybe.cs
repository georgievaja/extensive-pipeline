using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Extensive.Pipeline.CacheControl.Pure.Functors
{
    [PublicAPI]
    public sealed class Maybe<T>
    {
        internal bool HasItem { get; }
        internal T Item { get; }

        [NotNull] public static Maybe<T> None => new Maybe<T>();

        [Pure]
        [NotNull]
        public static Maybe<T> Some(T value)
        {
            return new Maybe<T>(value);
        }

        private Maybe()
        {
            this.HasItem = false;
        }

        private Maybe(T item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            this.HasItem = true;
            this.Item = item;
        }

        [Pure]
        [NotNull]
        public Maybe<TResult> Select<TResult>(
            [NotNull] Func<T, TResult> selector)
        {
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return this.HasItem ? new Maybe<TResult>(selector(this.Item)) : new Maybe<TResult>();
        }

        [Pure]
        [ItemNotNull]
        public async Task<Maybe<TResult>> SelectAsync<TResult>(
            [NotNull] Func<T, Task<TResult>> selector)
        {
            if (selector == null)
                throw new ArgumentNullException(nameof(selector));

            return this.HasItem
                ? new Maybe<TResult>(
                    await selector(this.Item).ConfigureAwait(false))
                : new Maybe<TResult>();
        }

        [Pure]
        [NotNull]
        public Maybe<TResult> SelectMany<TResult>(
            [NotNull] Func<T, Maybe<TResult>> selector)
        {
            if (selector == null)
                throw new ArgumentNullException(nameof(selector));

            return HasItem ? selector(this.Item) : new Maybe<TResult>();
        }

        [Pure]
        [ItemNotNull]
        public async Task<Maybe<TResult>> SelectManyAsync<TResult>(
            [NotNull] Func<T, Task<Maybe<TResult>>> selector)
        {
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            return HasItem ? await selector(this.Item).ConfigureAwait(false) : new Maybe<TResult>();
        }

        [Pure]
        [NotNull]
        public TResult MatchResult<TResult>(
            [NotNull] TResult nothing,
            [NotNull] Func<T, TResult> just)
        {
            if (nothing == null) throw new ArgumentNullException(nameof(nothing));
            if (just == null) throw new ArgumentNullException(nameof(just));

            return HasItem ? just(Item) : nothing;
        }

        [Pure]
        [ItemNotNull]
        public async Task<TResult> MatchResultAsync<TResult>(
            [NotNull] TResult nothing,
            [NotNull] Func<T, Task<TResult>> just)
        {
            if (nothing == null) throw new ArgumentNullException(nameof(nothing));
            if (just == null) throw new ArgumentNullException(nameof(just));

            return HasItem ? await just(Item).ConfigureAwait(false) : nothing;
        }

        [Pure]
        [NotNull]
        public TResult MatchResult<TResult>(
            [NotNull] TResult nothing,
            [NotNull] TResult just)
        {
            if (nothing == null) throw new ArgumentNullException(nameof(nothing));
            if (just == null) throw new ArgumentNullException(nameof(just));

            return HasItem ? just : nothing;
        }

        [Pure]
        [NotNull]
        public Func<TResult> Bind<T, TResult>(
            [NotNull] Func<T, TResult> func,
            [NotNull] T arg)
        {
            if (func == null) throw new ArgumentNullException(nameof(func));
            if (arg == null) throw new ArgumentNullException(nameof(arg));

            return () => func(arg);
        }

        public void Execute(
            [NotNull] Action<T> just)
        {
            if (just == null) throw new ArgumentNullException(nameof(just));

            if (HasItem)
            {
                just.Invoke(Item);
            }
        }

        public void Match(
            [NotNull] Action<T> just,
            [NotNull] Action nothing)
        {
            if (just == null) throw new ArgumentNullException(nameof(just));
            if (nothing == null) throw new ArgumentNullException(nameof(nothing));

            if (HasItem)
            {
                just.Invoke(Item);
            }
            else
            {
                nothing.Invoke();
            }
        }

        public async Task MatchAsync(
            [NotNull] Func<Task> just,
            [NotNull] Action nothing)
        {
            if (just == null) throw new ArgumentNullException(nameof(just));
            if (nothing == null) throw new ArgumentNullException(nameof(nothing));

            if (HasItem)
            {
                await (just.Invoke().ConfigureAwait(false));
            }
            else
            {
                nothing.Invoke();
            }
        }

        public async Task MatchAsync(
            [NotNull] Func<T, Task> just,
            [NotNull] Action nothing)
        {
            if (just == null) throw new ArgumentNullException(nameof(just));
            if (nothing == null) throw new ArgumentNullException(nameof(nothing));

            if (HasItem)
            {
                await just.Invoke(Item).ConfigureAwait(false);
            }
            else
            {
                nothing.Invoke();
            }
        }

        [Pure]
        [ItemNotNull]
        public async Task<TResult> MatchAsync<TResult>(
            [NotNull] Func<T, Task<TResult>> just,
            [NotNull] Func<Task<TResult>> nothing)
        {
            if (just == null) throw new ArgumentNullException(nameof(just));
            if (nothing == null) throw new ArgumentNullException(nameof(nothing));

            if (HasItem)
            {
                return await just.Invoke(Item).ConfigureAwait(false);
            }

            return await nothing.Invoke().ConfigureAwait(false);
        }

        public void Match(
            [NotNull] Action just,
            [NotNull] Action nothing)
        {
            if (just == null) throw new ArgumentNullException(nameof(just));
            if (nothing == null) throw new ArgumentNullException(nameof(nothing));

            if (HasItem)
            {
                just.Invoke();
            }
            else
            {
                nothing.Invoke();
            }
        }

        [Pure]
        public override bool Equals([CanBeNull] object obj)
        {
            if (!(obj is Maybe<T> other))
                return false;

            return Equals(this.Item, other.Item);
        }

        [Pure]
        public override int GetHashCode()
        {
            return this.HasItem ? this.Item.GetHashCode() : 0;
        }
    }
}