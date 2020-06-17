trigger = function()
    return gm_data().date.month == 9 and gm_data().date.day == 1
end

desc = function()
    return {'TASK_COLLECT_TAX_START_DESC', gm_data().chaoting.year_expect_tax }
end

options = 
{
    OPTION_1 = 
    {
        rate = 0.0002,

        desc = function (self)
            return {'TASK_COLLECT_TAX_START_OPTION_1',  gm_data():tax_collect_expect(self.rate) }
        end,

        selected = function (self)
            local task = gm_data().tasks:find('COLLECT_TAX')
            task.start = true

            gm_data():tax_collect_start(self.rate);

            for key,pop in pairs(gm_data().pops) do
                if(pop.def.is_tax == true) then
                    pop.buffers:set_valid("TAXED_LEVEL1")
                end
            end
        end

    },

    OPTION_2 = 
    {
        rate = 0.0005,

        desc = function (self)
            return {'TASK_COLLECT_TAX_START_OPTION_2',  gm_data():tax_collect_expect(self.rate) }
        end,

        selected = function (self)
            local task = gm_data().tasks:find('COLLECT_TAX')
            task.start = true

            gm_data():tax_collect_start(self.rate);

            for key,pop in pairs(gm_data().pops) do
                if(pop.def.is_tax == true) then
                    pop.buffers:set_valid("TAXED_LEVEL2")
                end
            end
        end

    },

    OPTION_3 = 
    {
        rate = 0.001,

        desc = function (self)
            return {'TASK_COLLECT_TAX_START_OPTION_3',  gm_data():tax_collect_expect(self.rate) }
        end,

        selected = function (self)
            local task = gm_data().tasks:find('COLLECT_TAX')
            task.start = true

            gm_data():tax_collect_start(self.rate);

            for key,pop in pairs(gm_data().pops) do
                if(pop.def.is_tax == true) then
                    pop.buffers:set_valid("TAXED_LEVEL3")
                end
            end
        end

    },

    OPTION_4 = 
    {
        rate = 0.002,

        desc = function (self)
            return {'TASK_COLLECT_TAX_START_OPTION_4',  gm_data():tax_collect_expect(self.rate) }
        end,

        selected = function (self)
            local task = gm_data().tasks:find('COLLECT_TAX')
            task.start = true

            gm_data():tax_collect_start(self.rate);

            for key,pop in pairs(gm_data().pops) do
                if(pop.def.is_tax == true) then
                    pop.buffers:set_valid("TAXED_LEVEL4")
                end
            end
        end

    },

    OPTION_5 = 
    {
        rate = 0.003,

        desc = function (self)
            return {'TASK_COLLECT_TAX_START_OPTION_5',  gm_data():tax_collect_expect(self.rate) }
        end,

        selected = function (self)
            local task = gm_data().tasks:find('COLLECT_TAX')
            task.start = true

            gm_data():tax_collect_start(self.rate);

            for key,pop in pairs(gm_data().pops) do
                if(pop.def.is_tax == true) then
                    pop.buffers:set_valid("TAXED_LEVEL5")
                end
            end
        end

    }
}