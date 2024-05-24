# [RhythmBase](../../RhythmToolkit.md).[LevelElements](../namespace/LevelElements.md).OrderedEventCollection\<T\> where T : BaseEvent
### [RhythmBase.dll](../assembly/RhythmBase.md)
节奏医生事件集合管理类。  
实现了 ICollection<T> 接口。  

## 构造

名称 | 说明
-|-
new() | 构造一个空集合。
new(IEnumerable\<T\> items) | 以给定一系列事件构造一个集合。

## 属性和字段
修饰 | 类型 | 名称 | 说明
-|-|-|-
readonly | int | Count | 返回事件的总数量。<br>实现ICollection\<T\>.Count接口。
readonly | bool | Isreadonly | 指示是否只读。<br>实现ICollection\<T\>.Isreadonly接口。
readonly | [RDBeat](../class/RDBeat.md) | Length | 返回所有事件所占的总节拍。

## 方法
修饰 | 类型 | 名称 | 说明
-|-|-|-
| | IEnumerable\<IGrouping\<String, T\>\> | GetTaggedEvents(string name, bool direct) | 以标签名获取标签事件。
| | | Add(T item) | 向事件集合添加事件。<br>实现ICollection\<T\>.Add(BaseEvent item)接口。
| | | AddRange(IEnumerable\<T\> item) | 向事件集合添加一系列事件。
| | bool | Remove(T item) | 移除事件。<br>实现ICollection\<T\>.Remove()接口。
| | | RemoveRange(IEnumerable\<T\> items) | 移除给定列表内的所有事件。
| | int | RemoveAll(Predicate\<T\> predicate) | 移除满足谓词的事件并返回移除的个数。
| | | Clear() | 清空Events集合。<br>实现ICollection\<T\>.Clear()接口
| | bool | Contains(T item) | 返回集合是否包含此事件。<br>实现ICollection\<T\>.Contains(T item)接口
| | IEnumerable\<T\> | Where(Func\<T, bool\> predicate) | 以谓词筛选指定事件。此迭代器以事件的时间顺序迭代。
| | IEnumerable\<T\> | Where(float startBeat, float endBeat) | 以节拍范围`[startBeat, endBeat)`筛选指定事件。此迭代器以事件的时间顺序迭代。
| | IEnumerable\<T\> | Where([RDBeat](../class/RDBeat.md) startBeat, [RDBeat](../class/RDBeat.md) endBeat) | 以节拍范围`[startBeat, endBeat)`筛选指定事件。此迭代器以事件的时间顺序迭代。
| | IEnumerable\<T\> | Where([RDRange](../class/RDRange.md) range) | 以节拍范围筛选指定事件。此迭代器以事件的时间顺序迭代。
| | IEnumerable\<T\> | Where(Range range) | 以小节范围筛选指定事件。此迭代器以事件的时间顺序迭代。
| | IEnumerable\<T\> | Where(Func\<T, bool\> predicate, float startBeat, float endBeat) | 以谓词和节拍范围`[startBeat, endBeat)`筛选指定事件。此迭代器以事件的时间顺序迭代。
| | IEnumerable\<T\> | Where(Func\<T, bool\> predicate, [RDBeat](../class/RDBeat.md) startBeat, [RDBeat](../class/RDBeat.md) endBeat) | 以谓词和节拍范围`[startBeat, endBeat)`筛选指定事件。此迭代器以事件的时间顺序迭代。
| | IEnumerable\<T\> | Where(Func\<T, bool\> predicate, [RDRange](../class/RDRange.md) range) | 以谓词和节拍范围筛选指定事件。此迭代器以事件的时间顺序迭代。
| | IEnumerable\<T\> | Where(Func\<T, bool\> predicate, Range range) | 以谓词和小节范围筛选指定事件。此迭代器以事件的时间顺序迭代。
| | IEnumerable\<U\> | Where\<U\>() where U : T | 以类型筛选指定事件。此迭代器以事件的时间顺序迭代。
| | IEnumerable\<U\> | Where\<U\>(Func\<U, bool\> predicate) where U : T | 以类型和谓词筛选指定事件。此迭代器以事件的时间顺序迭代。
| | IEnumerable\<U\> | Where\<U\>(float startBeat, float endBeat) where U : T | 以类型和节拍范围`[startBeat, endBeat)`筛选指定事件。此迭代器以事件的时间顺序迭代。
| | IEnumerable\<U\> | Where\<U\>([RDBeat](../class/RDBeat.md) startBeat, [RDBeat](../class/RDBeat.md) endBeat) where U : T | 以类型和节拍范围`[startBeat, endBeat)`筛选指定事件。此迭代器以事件的时间顺序迭代。
| | IEnumerable\<U\> | Where\<U\>([RDRange](../class/RDRange.md) range) where U : T | 以类型和小节范围筛选指定事件。此迭代器以事件的时间顺序迭代。
| | IEnumerable\<U\> | Where\<U\>(Range range) where U : T | 以类型和节拍范围筛选指定事件。此迭代器以事件的时间顺序迭代。
| | IEnumerable\<U\> | Where\<U\>(Func\<U, bool\> predicate, float startBeat, float endBeat) where U : T | 以类型、谓词和节拍范围`[startBeat, endBeat)`筛选指定事件。此迭代器以事件的时间顺序迭代。
| | IEnumerable\<U\> | Where\<U\>(Func\<U, bool\> predicate, [RDBeat](../class/RDBeat.md) startBeat, [RDBeat](../class/RDBeat.md) endBeat) where U : T | 以类型、谓词和节拍范围`[startBeat, endBeat)`筛选指定事件。此迭代器以事件的时间顺序迭代。
| | IEnumerable\<U\> | Where\<U\>(Func\<U, bool\> predicate, [RDRange](../class/RDRange.md) range) where U : T | 以类型、谓词和小节范围筛选指定事件。此迭代器以事件的时间顺序迭代。
| | IEnumerable\<U\> | Where\<U\>(Func\<U, bool\> predicate, Range range) where U : T | 以类型、谓词和节拍范围筛选指定事件。此迭代器以事件的时间顺序迭代。
| | T | First() | 获取集合内第一个事件。
| | T | First(Func\<T, bool\> predicate) | 获取集合内第一个满足谓词的事件。
| | T | First\<U\>() where U : T  | 获取集合内第一个满足类型的事件。
| | T | First\<U\>(Func\<U, bool\> predicate) where U : T | 获取集合内第一个满足谓词和类型的事件。
| | T | FirstOrDefault() | 获取集合内第一个事件。<br>若未找到则返回null。
| | T | FirstOrDefault(BaseEvent defaultValue) | 获取集合内第一个事件。<br>若未找到则返回defaultValue。
| | T | FirstOrDefault(Func\<T, bool\> predicate) | 获取集合内第一个满足谓词的事件。<br>若未找到则返回null。
| | T | FirstOrDefault(Func\<T, bool\> predicate, BaseEvent defaultValue) | 获取集合内第一个满足谓词的事件。<br>若未找到则返回defaultValue。
| | U | FirstOrDefault\<U\>() where U : T | 获取集合内第一个满足类型的事件。<br>若未找到则返回null。
| | U | FirstOrDefault\<U\>(BaseEvent defaultValue) where U : T | 获取集合内第一个满足类型的事件。<br>若未找到则返回defaultValue。
| | U | FirstOrDefault\<U\>(Func\<U, bool\> predicate) where U : T | 获取集合内第一个满足谓词和类型的事件。<br>若未找到则返回null。
| | U | FirstOrDefault\<U\>(Func\<U, bool\> predicate, T defaultValue) where U : T | 获取集合内第一个满足谓词和类型的事件。<br>若未找到则返回defaultValue。
| | T | Last() | 获取集合内第一个事件。
| | T | Last(Func\<T, bool\> predicate) | 获取集合内最后一个满足谓词的事件。
| | U | Last\<U\>() where U : T | 获取集合内最后一个满足类型的事件。
| | U | Last\<U\>(Func\<U, bool\> predicate) where U : T | 获取集合内最后一个满足谓词和类型的事件。
| | T | LastOrDefault() | 获取集合内最后一个事件。<br>若未找到则返回null。
| | T | LastOrDefault(T defaultValue) | 获取集合内最后一个事件。<br>若未找到则返回defaultValue。
| | T | LastOrDefault(Func\<T, bool\> predicate) | 获取集合内最后一个满足谓词的事件。<br>若未找到则返回null。
| | T | LastOrDefault(Func\<T, bool\> predicate, T defaultValue) | 获取集合内最后一个满足谓词的事件。<br>若未找到则返回defaultValue。
| | U | LastOrDefault\<U\>() where U : T | 获取集合内最后一个满足类型的事件。<br>若未找到则返回null。
| | U | LastOrDefault\<U\>(T defaultValue) where U : T | 获取集合内最后一个满足类型的事件。<br>若未找到则返回defaultValue。
| | U | LastOrDefault\<U\>(Func\<U, bool\> predicate) where U : T | 获取集合内最后一个满足谓词和类型的事件。<br>若未找到则返回null。
| | U | LastOrDefault\<U\>(Func\<U, bool\> predicate, T defaultValue) where U : T | 获取集合内最后一个满足谓词和类型的事件。<br>若未找到则返回defaultValue。
| | IEnumerable\<T\> | ExtractEventsAt(float beat) | 返回集合内指定节拍的事件集合。若此处无事件则返回空集合。<br>**注意：此方法在返回结果的同时会尝试移除集合内的相同事件。**
| | | CopyTo(T[] array, int arrayIndex) | 将事件拷贝到数组。<br>实现ICollection\<T\>.CopyTo()接口。
| | IEnumerble<T> | GetEnumerator() | 获取此集合实例的迭代器。此迭代器以事件的时间顺序迭代。

[E]: ../class/BaseEvent.md