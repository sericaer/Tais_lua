EVENT_DEF.DEPART.EVENT_DEPART_HARVEST_NULL = 
{
    options = 
    {
        OPTION_1 = 
        {
            selected = function ()
                for key,value in pair(gm_depart().pops) do
                    value.buffers:find('PINKUN').exist = true
                end
            end,
        }
    }
}