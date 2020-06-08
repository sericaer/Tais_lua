TASK_DEF.COLLECT_TAX = 
{
    tax_rate = { 0.0002, 0.0005, 0.001, 0.002, 0.003 },

    base_speed = 10,

    start_event = function(self)
       return 'EVENT_TASK_COLLECT_TAX_START' 
    end,

    finish_event = function (self)
        return ''    
    end,

    get_tax_rate = function (self)
        return self.tax_rate[self.level]
    end

}