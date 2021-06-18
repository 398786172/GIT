using Stateless;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace S
{
    //2.1创建状态机，LeaveStatus和LeaveAction都是枚举，leave（业务对象：请假的表单）是追踪状态的对象，状态的字段名叫Status
   // var _machine = new StateMachine<LeaveStatus, LeaveAction>(() => leave.Status, s => leave.Status = s);

    //2.2配置状态机，以下代码意思是当业务状态处于Draft状态时，可以发生Submit动作，动作之后状态变为UnderApprove
   // _machine.Configure(LeaveStatus.Draft).Permit(LeaveAction.Submit, LeaveStatus.UnderApprove);

    //2.3发生动作，action是枚举值（状态机会根据发生的动作改编成相应的状态，如果是没有定义的动作，会抛异常，这个抛异常可以重写，下面再说）
   // _machine.Fire(action);
 

//3、本人的一种封装，根据业务需要去自行查阅源码完善吧，代码应该不算难，都能看得懂

public class Leave
    {
        public long Id { get; set; }
        public LeaveStatus Status { get; set; }
        public DateTime? FirSaveTime { get; set; }
        public DateTime? SubmitTime { get; set; }
        public DateTime? PassTime { get; set; }
        public DateTime? RejectTime { get; set; }
    }

    public enum LeaveStatus
    {
        [Description("拒绝")]
        Rejected = -1,
        [Description("草稿")]
        Draft = 0,
        [Description("审批中")]
        UnderApprove = 1,
        [Description("审批通过")]
        Approved = 2
    }

    public enum LeaveAction
    {
        [Description("拒绝")]
        Reject = -1,
        [Description("保存")]
        Save = 0,
        [Description("提交")]
        Submit = 1,
        [Description("审批通过")]
        Pass = 2,
    }

    public class LeaveManager
    {
        private Leave _leave;
        StateMachine<LeaveStatus, LeaveAction> _machine;
        //StateMachine<LeaveStatus, LeaveAction>.TriggerWithParameters<string> _triggerWithPara;

        public LeaveManager(Leave leave)
        {
            _leave = leave;
            _machine = new StateMachine<LeaveStatus, LeaveAction>(() => leave.Status, s => leave.Status = s);//(() => leave.Status, s => leave.Status = s);
            _machine.OnUnhandledTrigger((status, action) => throw new Exception($"请假的状态为{status}，不可以执行{action}")); //重写抛异常的方法
            Configure();
        }

        public void Execute(LeaveAction action)
        {
            _machine.Fire(action);
        }

        private void Configure()
        {
            _machine.Configure(LeaveStatus.Draft)
//.OnEntry(OnSaveEntry)
               // .OnExit(OnSaveExit)
               // .PermitReentry(LeaveAction.Save)
                .Permit(LeaveAction.Submit, LeaveStatus.UnderApprove);
            //.Ignore(LeaveAction.Save);
            _machine.Configure(LeaveStatus.UnderApprove)
              //  .OnEntry(OnSubmitEntry)
              //  .OnExit(OnSubmitExit)
              //  .PermitReentry(LeaveAction.Submit)
                .Permit(LeaveAction.Save, LeaveStatus.Draft)
                .Permit(LeaveAction.Pass, LeaveStatus.Approved)
                .Permit(LeaveAction.Reject, LeaveStatus.Rejected);
        }

        private void OnSaveExit(StateMachine<LeaveStatus, LeaveAction>.Transition obj)
        {

        }

        private void OnSaveEntry(StateMachine<LeaveStatus, LeaveAction>.Transition obj)
        {
            if (!_leave.FirSaveTime.HasValue)
            {
                _leave.FirSaveTime = DateTime.Now;
            }
        }

        private void OnSubmitExit(StateMachine<LeaveStatus, LeaveAction>.Transition obj)
        {

        }

        private void OnSubmitEntry(StateMachine<LeaveStatus, LeaveAction>.Transition obj)
        {
            if (!_leave.SubmitTime.HasValue)
            {
                _leave.SubmitTime = DateTime.Now;
            }
        }
    }
}
