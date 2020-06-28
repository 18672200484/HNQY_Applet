 
/*汽车衡监控界面*/
 
 var CarSamplerV8Cef;
    if (!CarSamplerV8Cef)CarSamplerV8Cef = {};

    (function() {
        // 急停
      CarSamplerV8Cef.Stop = function(paramSampler,CarSampleNum) {
        native function Stop(paramSampler,CarSampleNum);
        Stop(paramSampler,CarSampleNum);
      };   
              // 运行
      CarSamplerV8Cef.Start = function(paramSampler,CarSampleNum) {
        native function Start(paramSampler,CarSampleNum);
        Start(paramSampler,CarSampleNum);
      };  
      
        // 车辆信息
      CarSamplerV8Cef.CarInfo = function(paramSampler) {
        native function CarInfo(paramSampler);
        CarInfo(paramSampler);
      };   
      
        // 故障复位
      CarSamplerV8Cef.ErrorReset = function(paramSampler,CarSampleNum) {
        native function ErrorReset(paramSampler,CarSampleNum);
        ErrorReset(paramSampler,CarSampleNum);
      };  
      
        // 设备复位
      CarSamplerV8Cef.DeviceReset = function(paramSampler,CarSampleNum) {
        native function DeviceReset(paramSampler,CarSampleNum);
        DeviceReset(paramSampler,CarSampleNum);
      };   
      
        // 采样历史记录
      CarSamplerV8Cef.SampleHistory = function(paramSampler) {
        native function SampleHistory(paramSampler);
        SampleHistory(paramSampler);
      };   

      //查看故障信息
      CarSamplerV8Cef.GetHitchs=function(paramSampler,CarSampleNum){
      native function GetHitchs(paramSampler,CarSampleNum);
      return GetHitchs(paramSampler,CarSampleNum);
      };
    })(); 