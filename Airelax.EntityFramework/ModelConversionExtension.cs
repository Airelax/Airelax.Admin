using System;
using System.Linq.Expressions;
using Airelax.Domain.DomainObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SpanJson;

namespace Airelax.EntityFramework
{
    public static class ModelConversionExtension
    {
        public static PropertyBuilder<T> HasJsonConversion<T>(this PropertyBuilder<T> propertyBuilder) where T : class, new()
        {
            var converter = new ValueConverter<T, string>
            (
                value => JsonSerializer.Generic.Utf16.Serialize(value),
                value => JsonSerializer.Generic.Utf16.Deserialize<T>(value) ?? new T()
            );

            var comparer = new ValueComparer<T>
            (
                (l, r) => JsonSerializer.Generic.Utf16.Serialize(l) == JsonSerializer.Generic.Utf16.Serialize(r),
                v => v == null ? 0 : JsonSerializer.Generic.Utf16.Serialize(v).GetHashCode(),
                v => JsonSerializer.Generic.Utf16.Deserialize<T>(JsonSerializer.Generic.Utf16.Serialize(v))
            );

            propertyBuilder.HasConversion(converter);
            propertyBuilder.Metadata.SetValueConverter(converter);
            propertyBuilder.Metadata.SetValueComparer(comparer);
            //propertyBuilder.HasColumnType(Define.SqlServer.MAX_NVARCHAR);

            return propertyBuilder;
        }

        public static PropertyBuilder<TProp> SetEnumDbMapping<T, TProp>(this EntityTypeBuilder<T> builder,
            Expression<Func<T, TProp>> propSelector) where T : class where TProp : Enum
        {
            return builder.Property(propSelector).HasConversion(
                v => Convert.ToInt32(v), v => (TProp) (object) v);
        }

        public static PropertyBuilder<TProp?> SetEnumDbMapping<T, TProp>(this EntityTypeBuilder<T> builder,
            Expression<Func<T, TProp?>> propSelector) where T : class where TProp : struct, Enum
        {
            return builder.Property(propSelector).HasConversion(
                v => v == null ? (int?) null : Convert.ToInt32(v), v => v == null ? null : (TProp) (object) v);
        }


        public static PropertyBuilder<TProp> SetPropMaxLength<T, TProp>(this EntityTypeBuilder<T> builder, Expression<Func<T, TProp>> propSelector,
            int maxLength) where T : class
        {
            return builder.Property(propSelector).HasMaxLength(maxLength);
        }

        public static void SetEntityKey<TEntity, TId>(this EntityTypeBuilder<TEntity> builder) where TEntity : Entity<TId>
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever().IsRequired();
        }

        // public static ReferenceReferenceBuilder SetZeroToOneRelation<TZero, TOne, TRelationEntity>(this EntityTypeBuilder<TZero> entityTypeBuilder,
        //     Expression<Func<TOne, TRelationEntity>> navigationExpression, Expression<Func<TOne, object>> foreignKeyExpression)
        //     where TZero : class where TOne : class
        // {
        //     entityTypeBuilder.HasOne<TOne>().WithOne(navigationExpression).HasForeignKey<TZero>(foreignKeyExpression);
        // }
    }
}