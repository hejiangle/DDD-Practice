# DDD-Practice
## Parking Lot 小练习
### 需求
对于停车场
- 可以停车、取车
- 停车后获得小票，用小票可以取车
- 不能停过多的车
- 无效的小票无法取车
- 可能有多个停车车
对于泊车小弟
- 管理多个停车场
- 按照顺序依次停到停车场

### 分析、设计

![简单的设计图（第一版）](https://github.com/hejiangle/DDD-Practice/blob/master/Parking%20Lot.png?raw=true)

### 代码说明

技术栈： ASP.NET Core 3.1 + XUnit(Tests) + Moq(Mock) + Rider(IDE)

目前代码属于半成品，命令行主程序未完成。
如果想了解主要业务功能部分，可以通过运行 ParkingLot.Tests 项目中的集成测试完成几种场景的功能验证。

需求相关的业务逻辑都体现在 Domain 层.
另外提供了 Infrastructure 层，模拟一些数据操作（获取、存储等）。

未完成部分： 
- 异常处理
- 命令行输出
- 少量重构
