﻿using System.Collections;
using System.Reflection;

namespace BO;

static class Tools
{
    public static string ToStringProperty<T>(T? t)
    {
        if (t == null) return string.Empty;

        string str = "";
        foreach (PropertyInfo item in t.GetType().GetProperties())
        {
            object? value = item.GetValue(t, null);
            if (value is IEnumerable enumerable && !(value is string))
            {
                foreach (var listItem in enumerable)
                {
                    str += "\n" + item.Name + ": " + listItem;
                }
            }
            else
            {
                str += "\n" + item.Name + ": " + value;
            }
        }

        return str;
    }


    public static Target CopySimilarFields<Source, Target>(this Source source, object[] objects = null!) where Target : new()
    {
        Target target = new Target();

        Dictionary<string, PropertyInfo> propertiesInfoTarget = target.GetType().GetProperties()
                 .ToDictionary(p => p.Name, p => p); 

        IEnumerable<PropertyInfo> propertiesInfoSource = source!.GetType().GetProperties();

        try
        {
            foreach (var propertyInfo in propertiesInfoSource)
            {

                if (propertiesInfoTarget.TryGetValue(propertyInfo.Name, out var value)
                    && (propertyInfo.PropertyType == typeof(string) || !(propertyInfo.PropertyType.IsClass)))
                {

                    value.SetValue(target, propertyInfo.GetValue(source));
                }

            }

            addOtherPropertiesValues(objects, target, propertiesInfoTarget);

            return target;
        }
        catch (Exception ex) {
            throw new BlGeneralExceptionException(ex.Message);
                }
    }

    private static void addOtherPropertiesValues<Target>(object[] objects, Target target, Dictionary<string, PropertyInfo> propertiesInfoTarget) where Target : new()
    {
        if (objects != null)
        {
            foreach (var obj in objects)
            {
                if (propertiesInfoTarget.TryGetValue(obj.GetType().Name, out var value))
                {
                    value.SetValue(target, obj);
                }
            }
        }
    }

    public static IEnumerable<Target> CopySimilarFieldsList<Source, Target>(this IEnumerable<Source> sources) where Target : new() =>
        from source in sources
        select source.CopySimilarFields<Source, Target>();

    public static TaskStatus SetStatus(this DO.Task t)
    {
        if (t.EndTask.HasValue && t.EndTask < DateTime.Now)
            return TaskStatus.Done;
        if (!t.ScheduleStart.HasValue)
            return TaskStatus.Unscheduled;
        if (t.ScheduleStart.HasValue && !t.StartTask.HasValue)
            return TaskStatus.Scheduled;
        if (t.StartTask.HasValue && !t.EndTask.HasValue)
            return TaskStatus.OnTrack;
        return TaskStatus.Unscheduled;

    }
    public static bool IsAllLetters(string s)
    {
        foreach (char c in s)
        {
            if (Char.IsDigit(c))
                
                return false;
        }
        return true;
    }
   
    public static bool IsAllDigits(string s)
    {
        foreach (char c in s)
        {
            if (!Char.IsDigit(c))
                return false;
        }
        return true;
    }
}

