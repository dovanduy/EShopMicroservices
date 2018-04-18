﻿using Articles.WriteSide.Events.ToSaga;
using Infrastructure.Contracts;
using Infrastructure.Domain;
using System;

namespace Articles.WriteSide.Aggregates
{
	public class Comment :
		AggregateRoot,
		IHandle<CommentDeletedEvent>,
		IHandle<SagaCommentInsertedEvent>,
		IHandle<CommentUpdatedEvent>
	{
		public DateTime AddedDate { get; set; }
		public string AddedBy { get; set; }
		public string AddedByEmail { get; set; }
		public string AddedByIp { get; set; }
		public string Body { get; set; }
		public Guid ArticleId { get; set; }
		public Article Article { get; set; }

		public Comment()
		{
		}

		public Comment(
			Guid id,
			DateTime AddedDate,
			string AddedBy,
			string AddedByEmail,
			string AddedByIp,
			string Body,
			Guid ArticleId)
		{
			var @event = new SagaCommentInsertedEvent
			{
				AggregateId = id,
				AddedBy = AddedBy,
				AddedByEmail = AddedByEmail,
				AddedByIp = AddedByIp,
				AddedDate = AddedDate,
				ArticleId = ArticleId,
				Body = Body
			};
			ApplyChange(@event);
		}

		public void Delete()
		{
			var @event = new CommentDeletedEvent
			{
				AggregateId = Id
			};
			ApplyChange(@event);
		}

		public void Update()
		{
			var @event = new CommentUpdatedEvent
			{
				AggregateId = Id,
				Body = Body
			};
			ApplyChange(@event);
		}

		public void Handle(CommentDeletedEvent @event)
		{
			Id = @event.AggregateId;
		}

		public void Handle(SagaCommentInsertedEvent @event)
		{
			AddedBy = @event.AddedBy;
			AddedByEmail = @event.AddedByEmail;
			AddedByIp = @event.AddedByIp;
			AddedDate = @event.AddedDate;
			Id = @event.AggregateId;
			ArticleId = @event.ArticleId;
			Body = @event.Body;
		}

		public void Handle(CommentUpdatedEvent @event)
		{
			Id = @event.AggregateId;
			Body = @event.Body;
		}
	}
}
