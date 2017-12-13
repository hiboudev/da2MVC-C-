﻿using da2mvc.core.events;
using da2mvc.framework.collection.events;
using da2mvc.framework.model;
using System.Collections.Generic;

namespace da2mvc.framework.collection.model
{
    public class CollectionModel<ModelType> : EventDispatcher where ModelType : IModel
    {
        public const string EVENT_ITEMS_ADDED = "itemsAdded";
        public const string EVENT_ITEMS_REMOVED = "itemsRemoved";
        public const string EVENT_CLEARED = "cleared";

        public List<ModelType> Items { get; } = new List<ModelType>();

        virtual public void Add(ModelType model)
        {
            Items.Add(model);
            DispatchEvent(new CollectionEventArgs<ModelType>(EVENT_ITEMS_ADDED, this, new ModelType[] { model }));
        }

        virtual public void AddRange(List<ModelType> models)
        {
            Items.AddRange(models);
            DispatchEvent(new CollectionEventArgs<ModelType>(EVENT_ITEMS_ADDED, this, models.ToArray()));
        }

        virtual public void Remove(ModelType model)
        {
            Items.Remove(model);
            DispatchEvent(new CollectionEventArgs<ModelType>(EVENT_ITEMS_REMOVED, this, new ModelType[] { model }));
        }

        virtual public void Clear()
        {
            var itemsCopy = Items.ToArray();
            Items.Clear();
            DispatchEvent(new CollectionEventArgs<ModelType>(EVENT_CLEARED, this, itemsCopy));
        }

        public ModelType Get(int id)
        {
            foreach (ModelType model in Items)
                if (model.Id == id)
                    return model;
            return default(ModelType);
        }
    }
}