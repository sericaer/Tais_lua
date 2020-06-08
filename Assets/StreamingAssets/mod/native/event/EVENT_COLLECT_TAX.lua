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
            rate = 0.001,

            desc = function (self)
                return  gm_data().tax_expect:expect(self.rate);
            end,

            selected = function (self)
                local task = gm_data().tasks:find('COLLECT_TAX')
                task.start = true

                gm_data().tax_expect:start(self.rate);

                for key,pop in pairs(gm_data().pops) do
                    if(pop.def.is_tax == true) then
                        pop.buffers:find("TAXED_LEVEL1").exist = true;
                    end
                end
            end

        }
    }
}