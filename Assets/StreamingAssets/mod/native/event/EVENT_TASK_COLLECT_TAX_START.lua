EVENT_DEF.GLOBAL.EVENT_COLLECT_TAX_START = 
{
    occur_rate = function ()
        if gm_data().date.month == 9 and gm_data().date.day == 1 then
            return 1
        end
        return 0
    end,


    options = 
    {
        OPTION_1 = 
        {
            rate = 0.0002,

            desc = function (self)
                return  gm_data():tax_collect_expect(self.rate);
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
                return  gm_data():tax_collect_expect(self.rate);
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
                return  gm_data():tax_collect_expect(self.rate);
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
                return  gm_data():tax_collect_expect(self.rate);
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
                return  gm_data():tax_collect_expect(self.rate);
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
}