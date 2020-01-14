# [GeoSOT][1]全球等经纬度剖分网格编码/解码算法开源实现
  
GeoSOT编码模型本质上是整型一维二进制编码，可用于建立空间相关数据编码索引，将空间查询、统计分析转换为编码运算，从而在不同尺度上实现统计分析成果的快速聚合。  

***
功能特性：  
1 支持从度分秒经纬度构建剖分块对象；  
2 支持从SOT一维编码构建剖分块对象；  
3 支持剖分块特征角点坐标计算；  
4 支持剖分块范围四至计算；  
5 支持剖分块层、行、列号计算  
***

示例 1： 从DMS经纬度构建Tile对象  
```csharp
var dms = "39° 54' 37.0098\" N, 116° 18' 54.8198\" E";  
var _tile = new Tile(dms, 15);  
Console.WriteLine(_tile.ToString());  
// print G001310322-230230
```

示例 2： 从编码构建Tile对象  
```csharp
var 1dCode = "G001110221-021123-021123.02203010012";  
var _tile = new Tile(1dCode);  
Console.WriteLine(_tile.ToString());  
// print "G001110221-021123-021123.02203010012"  
```

示例 3： 特征角点坐标获取  
```csharp
var dms = "39° 54' 37.0098\" N, 116° 18' 54.8198\" E";  
var _tile = new Tile(dms, 15);  
Console.WriteLine(_tile.CornerLng);  
Console.WriteLine(_tile.CornerLat);  
// print 116.3 39.9  
```

示例 4： 范围四至计算  
```csharp
var dms = "39° 54' 37.0098\" N, 116° 18' 54.8198\" E";  
var _tile = new Tile(dms, 15);  
Console.WriteLine(_tile.GetBbox().ToString());  
// print 116.3 39.9 116.316667 39.916667  
```

示例 5： 层、行、列号计算  
```csharp
var dms = "39° 54' 37.0098\" N, 116° 18' 54.8198\" E";  
var _tile = new Tile(dms, 15);  
Console.WriteLine(_tile.Level);  
Console.WriteLine(_tile.X);  
Console.WriteLine(_tile.Y);  
// print 15 7442 2550  
```

_[1]: 论文引用:GeoSOT：基于2^n及整型一维数组的全球经纬度剖分网格({Geo}-graphical coordinates {S}ubdividing grid with {O}ne dimension integral coding on 2n-{T}ree)_